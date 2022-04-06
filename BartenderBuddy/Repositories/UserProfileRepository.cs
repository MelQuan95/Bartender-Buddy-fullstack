using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using BartenderBuddy.Models;
using BartenderBuddy.Utils;

namespace BartenderBuddy.Repositories
{
    public class UserProfileRepository : BaseRepository, IUserProfileRepository
    {
        public UserProfileRepository(IConfiguration configuration) : base(configuration) { }

        public UserProfile GetByEmail(string email)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT up.Id, up.full_name, 
                              up.email, up.created_at
                          FROM UserProfile up
                              
                         WHERE Email = @email";

                    DbUtils.AddParameter(cmd, "@email", email);

                    UserProfile userProfile = null;

                    var reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        userProfile = new UserProfile()
                        {
                            Id = DbUtils.GetInt(reader, "Id"),
                            FullName = DbUtils.GetString(reader, "full_name"),
                            Email = DbUtils.GetString(reader, "email"),
                            CreatedDate = DbUtils.GetDateTime(reader, "created_at"),
                           
                           
                        };
                    }
                    reader.Close();

                    return userProfile;
                }
            }
        }
        public void Add(UserProfile userProfile)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO UserProfile (full_name, Email, created_at)
                                        OUTPUT INSERTED.ID
                                        VALUES (@full_name, 
                                                @email, @created_at)";
                    
                    DbUtils.AddParameter(cmd, "@full_name", userProfile.FullName);
                    DbUtils.AddParameter(cmd, "@email", userProfile.Email);
                    DbUtils.AddParameter(cmd, "@created_at", DateTime.Now);



                    userProfile.Id = (int)cmd.ExecuteScalar();
                }
            }
        }

    }
}