using System;
using System.Collections.Generic;
using System.Linq;
using Covid.Database;
using Covid.Models.Entities;
using Covid.Models.Common;
using Microsoft.EntityFrameworkCore;

namespace Covid.Services.UserService
{
    public class UserService
    {
        private readonly ApplicationDbContext _dbContext;

        public UserService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        #region - Admin -

        public ServiceResult<IList<User>> AdminPullUsers()
        {
            try
            {
                List<User> users = _dbContext.Users.ToList();

                return new ServiceResult<IList<User>> { Value = users };
            }
            catch (Exception exception)
            {
                return new ServiceResult<IList<User>> { Exception = exception };
            }
        }

        public ServiceResult<IList<Match>> AdminPullMatches()
        {
            try
            {
                List<Match> matches = _dbContext.Matches.Include(x => x.UserX)
                                                        .Include(x => x.UserY)
                                                        .OrderBy(x => x.UserX.Key).ToList();

                return new ServiceResult<IList<Match>> { Value = matches };
            }
            catch (Exception exception)
            {
                return new ServiceResult<IList<Match>> { Exception = exception };
            }
        }

        public ServiceResult<IList<Alert>> AdminPullAlerts()
        {
            try
            {
                List<Alert> alerts = _dbContext.Alerts.Include(x => x.User)
                                                      .OrderByDescending(x => x.When)
                                                      .ToList();

                return new ServiceResult<IList<Alert>> { Value = alerts };
            }
            catch (Exception exception)
            {
                return new ServiceResult<IList<Alert>> { Exception = exception };
            }
        }

        #endregion

        #region - Subscription -

        public ServiceResult<User> Subscribe()
        {
            try
            {
                User user = new User();

                _dbContext.Users.Add(user);
                _dbContext.SaveChanges();

                return new ServiceResult<User> { Value = user };
            }
            catch (Exception exception)
            {
                return new ServiceResult<User> { Exception = exception };
            }
        }

        public ServiceResult<bool> Unsubcribe(string Key)
        {
            try
            {
                User user = _dbContext.Users.FirstOrDefault();
                Match[] matches = _dbContext.Matches.Where(x => x.UserX.Key == user.Key || x.UserY.Key == user.Key).ToArray();

                _dbContext.Matches.RemoveRange(matches);
                _dbContext.Users.Remove(user);
                _dbContext.SaveChanges();

                return new ServiceResult<bool> { Value = true };
            }
            catch (Exception exception)
            {
                return new ServiceResult<bool> { Exception = exception };
            }
        }

        #endregion

        #region - Matches -

        public ServiceResult<Match> PushMatch(string Key, string matchedKey, DateTime when)
        {
            try
            {
                User user = _dbContext.Users.FirstOrDefault(x => x.Key == Key);
                if (user == null) throw new Exception($"Following Key not found: { Key }");

                User matchedUser = _dbContext.Users.FirstOrDefault(x => x.Key == matchedKey);
                if (matchedUser == null) throw new Exception($"Following Key not found: { matchedKey }");

                Match match = new Match { UserX = user, UserY = matchedUser, When = when };

                _dbContext.Matches.Add(match);
                _dbContext.SaveChanges();

                return new ServiceResult<Match> { Value = match };
            }
            catch (Exception exception)
            {
                return new ServiceResult<Match> { Exception = exception };
            }
        }

        public ServiceResult<IList<Match>> PullMatches(string Key)
        {
            try
            {
                List<Match> matches = _dbContext.Matches.Include(x => x.UserX)
                                                        .Include(x => x.UserY)
                                                        .Where(x => x.UserX.Key == Key || x.UserY.Key == Key)
                                                        .OrderBy(x => x.UserX.Key)
                                                        .ToList();

                return new ServiceResult<IList<Match>> { Value = matches };
            }
            catch (Exception exception)
            {
                return new ServiceResult<IList<Match>> { Exception = exception };
            }
        }

        #endregion

        #region - Alerts -

        public ServiceResult<bool> PushAlert(string Key, DateTime when)
        {
            try
            {
                User user = _dbContext.Users.Include(x => x.Alerts).FirstOrDefault(x => x.Key == Key);
                if (user == null) throw new Exception($"Following Key not found: { Key }");

                if (user.Alerts.Count <= 0)
                {
                    Alert alert = new Alert { User = user, When = when };

                    _dbContext.Alerts.Add(alert);
                    _dbContext.SaveChanges();
                }
                else
                {
                    throw new Exception("An alert is already active for that Key");
                }

                return new ServiceResult<bool> { Value = true };
            }
            catch (Exception exception)
            {
                return new ServiceResult<bool> { Exception = exception };
            }
        }

        public ServiceResult<IList<Alert>> PullAlerts(string Key)
        {
            try
            {
                User user = _dbContext.Users.FirstOrDefault(x => x.Key == Key);
                if (user == null) throw new Exception($"Following Key not found: { Key }");

                List<Alert> alerts = _dbContext.Alerts.Where(x => x.User.Key == Key).OrderByDescending(x => x.When).ToList();

                return new ServiceResult<IList<Alert>> { Value = alerts };
            }
            catch (Exception exception)
            {
                return new ServiceResult<IList<Alert>> { Exception = exception };
            }
        }

        public ServiceResult<bool> RemoveAlert(string Key)
        {
            try
            {
                User user = _dbContext.Users.Include(x => x.Alerts).FirstOrDefault(x => x.Key == Key);
                if (user == null) throw new Exception($"Following Key not found: { Key }");

                user.Alerts.Clear();
                _dbContext.SaveChanges();

                return new ServiceResult<bool> { Value = true };
            }
            catch (Exception exception)
            {
                return new ServiceResult<bool> { Exception = exception };
            }
        }

        #endregion

        #region - Infections -

        public ServiceResult<IList<DateTime>> PullInfections(string Key, int incubation)
        {
            try
            {
                // Assume that incubation period is fixed at 15 days.
                // Keep all user matches that occured for 15 days before today. As 15 days is the incubation period, we assume that even if an infected match had accured before that period, the user is safe.
                var matches = _dbContext.Matches.Include(match => match.UserX.Alerts).Include(match => match.UserY.Alerts).Where(match => match.When >= DateTime.Now.AddDays(-incubation));

                // Amongs all 15 days last matches the user had with other people, we only retains matches with those who issued an alert less than 15 days after the recorded match.
                var infectedPeopleMatchedByUser = matches.Where(match => match.UserX.Key == Key);
                infectedPeopleMatchedByUser = infectedPeopleMatchedByUser.Where(match => match.UserY.Alerts.Count(alert => alert.When.AddDays(-incubation) < match.When) > 0);

                // Amongs all 15 days last matches peoples had with the user, we only retains thoses from other users who issued an alert less than 15 days after the recorded match.
                var userMatchedByInfectedPeople = matches.Where(match => match.UserY.Key == Key);
                userMatchedByInfectedPeople = userMatchedByInfectedPeople.Where(match => match.UserX.Alerts.Count(alert => alert.When.AddDays(-incubation) < match.When) > 0);

                // Just keep list of potention infection dates without duplicates and order them by most recent to older
                List<DateTime> infectionDates = new List<DateTime>();
                infectionDates.AddRange(infectedPeopleMatchedByUser.Select(x => x.When).ToList());
                infectionDates.AddRange(userMatchedByInfectedPeople.Select(x => x.When).ToList());
                infectionDates.Distinct().OrderByDescending(x => x);

                return new ServiceResult<IList<DateTime>> { Value = infectionDates };
            }
            catch (Exception exception)
            {
                return new ServiceResult<IList<DateTime>> { Exception = exception };
            }
        }

        #endregion
    }
}
