using System;
using Microsoft.Extensions.Configuration;
using BartenderBuddy.Models;
using BartenderBuddy.Utils;
using System.Collections.Generic;

namespace BartenderBuddy.Repositories
{
    public class SpiritRepository : BaseRepository, ISpiritRepository
    {
        public SpiritRepository(IConfiguration configuration) : base(configuration) { }

        public List<Spirits> GetAll()
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                          SELECT Id, Content
                            FROM Spirits
                        ";

                    var reader = cmd.ExecuteReader();

                    var spirits = new List<Spirits>();
                    while (reader.Read())
                    {
                        spirits.Add(new Spirits()
                        {
                            Id = DbUtils.GetInt(reader, "Id"),
                            content = DbUtils.GetString(reader, "content")
                        });
                    }

                    reader.Close();

                    return spirits;
                }
            }
        }

        public Spirits GetSpiritsById(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();

                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT Id, [content]
                    FROM Spirits
                    WHERE Id=@id";

                    DbUtils.AddParameter(cmd, "@id", id);
                    var reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        Spirits spirits = new Spirits()
                        {
                            Id = DbUtils.GetInt(reader, "Id"),
                            content = DbUtils.GetString(reader, "content")
                        };

                        reader.Close();
                        return spirits;
                    }

                    reader.Close();
                    return null;

                }
            }
        }

        public void Add(Spirits spirits)

        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO Spirits (content)
                                        OUTPUT INSERTED.ID
                                        VALUES (@content)";

                    DbUtils.AddParameter(cmd, "@content", spirits.content);




                    spirits.Id = (int)cmd.ExecuteScalar();
                }
            }
        }

        public void Update(Spirits spirits)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        UPDATE Spirits
                           SET content = @content
                         WHERE Id = @Id";

                    DbUtils.AddParameter(cmd, "@content", spirits.content);
                    DbUtils.AddParameter(cmd, "@Id", spirits.Id);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Delete(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "DELETE FROM Spirits WHERE Id = @Id";
                    DbUtils.AddParameter(cmd, "@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }

}