using PlayListApp.Models.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace PlayListApp.Models.Seeds
{
    public static class DefaultData
    {
        public static Task seedSongs(ApplicationDbContext _context)
        {
            try
            {
                var isSingerExist = _context.Singers.Any();
                if (isSingerExist)
                {
                    var amrDiab = _context.Singers.Where(s => s.Name.Equals("Amr Diab"));
                    var essaily = _context.Singers.Where(s => s.Name.Equals("Elessaily"));

                    var isSongsExist = _context.Songs.Any();
                    if (!isSongsExist && amrDiab != null && essaily != null)
                    {
                        var songs = new[]
                        {
                            new Songs{Id = Guid.NewGuid(), Name = "Makank", Type = SongTypeEnum.Pop , SingerId = amrDiab.FirstOrDefault().Id},
                            new Songs{Id = Guid.NewGuid(), Name = "Men kan ysada2", Type = SongTypeEnum.Jazz, SingerId = essaily.FirstOrDefault().Id},
                            new Songs{Id = Guid.NewGuid(), Name = "Matt3awadsh", Type = SongTypeEnum.Electronic, SingerId = amrDiab.FirstOrDefault().Id}
                        };
                        _context.Songs.AddRange(songs);
                        _context.SaveChanges();
                    }
                }
                
                return Task.CompletedTask;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static Task seedSingers(ApplicationDbContext _context)
        {
            try
            {
                var isSingersExisting = _context.Singers.Any();

                if (!isSingersExisting)
                {
                    var amrDiabImagePath = "../wwwroot/Images/Singers/Amr Diab/35D9A83D-562B-486F-AA21-F8E7BFF0E493.jpeg";
                    byte[] amrDiabImageBytes = System.IO.File.ReadAllBytes(amrDiabImagePath);

                    var essailyImagePath = "../wwwroot/Images/Singers/Elessaily/OIP1.jpg";
                    byte[] essailyImageBytes = System.IO.File.ReadAllBytes(essailyImagePath);

                    var Singers = new[]
                    {
                    new Singer{Id = Guid.NewGuid(), Name = "Amr Diab", Image = amrDiabImageBytes },
                    new Singer{Id = Guid.NewGuid(), Name = "Elessaily", Image = essailyImageBytes},
                    };
                    _context.Singers.AddRange(Singers);
                    _context.SaveChanges();
                }
                
                return Task.CompletedTask;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}