using System;
using System.IO;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using odec.Framework.Logging;
using odec.Server.Model.Attachment;
using odec.Server.Model.Attachment.Extended;
using odec.Server.Model.Secure.Attachment;
using odec.Server.Model.Secure.Attachment.Contexts;
using odec.Server.Model.User;
using AttachmentN = odec.Server.Model.Attachment.Attachment;

namespace Attachment.DAL.Tests
{
    internal class AttachmentTestHelper
    {
        public static void PopulateSecurityAttachmentCtx(SecureAttachmentContext db)
        {
            try
            {
               // db.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
                //db.ChangeTracker.
                var crafter = new Role
                {
                    Name = "Crafter"

                };
                db.Set<Role>().Add(crafter);
                var userRole = new Role
                {
                    Name = "User",
                };
                db.Set<Role>().Add(userRole);
                var jpg = new Extension
                {
                    Code = "JPG",
                    Name = "jpg",
                    IsActive = true,
                    SortOrder = 0,
                    DateCreated = DateTime.Now
                };
                if (!db.Extensions.AsNoTracking().Any(it => it.Code.Equals(jpg.Code)))
                    db.Extensions.Add(jpg);

                var jpeg = new Extension
                {
                    Code = "JPEG",
                    Name = "jpeg",
                    IsActive = true,
                    SortOrder = 0,
                    DateCreated = DateTime.Now
                };
                if (!db.Extensions.AsNoTracking().Any(it => it.Code.Equals(jpeg.Code)))
                    db.Extensions.Add(jpeg);

                var tiff = new Extension
                {
                    Code = "TIFF",
                    Name = "tiff",
                    IsActive = true,
                    SortOrder = 0,
                    DateCreated = DateTime.Now
                };
                if (!db.Extensions.AsNoTracking().Any(it => it.Code.Equals(tiff.Code)))
                    db.Extensions.Add(tiff);

                var bmp = new Extension
                {
                    Code = "BMP",
                    Name = "bmp",
                    IsActive = true,
                    SortOrder = 0,
                    DateCreated = DateTime.Now
                };
                if (!db.Extensions.AsNoTracking().Any(it => it.Code.Equals(bmp.Code)))
                    db.Extensions.Add(bmp);

                var png = new Extension
                {
                    Code = "PNG",
                    Name = "png",
                    IsActive = true,
                    SortOrder = 0,
                    DateCreated = DateTime.Now
                };
                if (!db.Extensions.AsNoTracking().Any(it => it.Code.Equals(png.Code)))
                    db.Extensions.Add(png);

                var txt = new Extension
                {
                    Code = "TXT",
                    Name = "txt",
                    IsActive = true,
                    SortOrder = 0,
                    DateCreated = DateTime.Now
                };
                if (!db.Extensions.AsNoTracking().Any(it => it.Code.Equals(txt.Code)))
                    db.Extensions.Add(txt);

                var gif = new Extension
                {
                    Code = "GIF",
                    Name = "gif",
                    IsActive = true,
                    SortOrder = 0,
                    DateCreated = DateTime.Now
                };
                if (!db.Extensions.AsNoTracking().Any(it => it.Code.Equals(gif.Code)))
                    db.Extensions.Add(gif);

                var docx = new Extension
                {
                    Code = "DOCX",
                    Name = "docx",
                    IsActive = true,
                    SortOrder = 0,
                    DateCreated = DateTime.Now
                };
                if (!db.Extensions.AsNoTracking().Any(it => it.Code.Equals(docx.Code)))
                    db.Extensions.Add(docx);

                var doc = new Extension
                {
                    Code = "DOC",
                    Name = "doc",
                    IsActive = true,
                    SortOrder = 0,
                    DateCreated = DateTime.Now
                };
                if (!db.Extensions.AsNoTracking().Any(it => it.Code.Equals(doc.Code)))
                    db.Extensions.Add(doc);

                var xlsx = new Extension
                {
                    Code = "XLSX",
                    Name = "xlsx",
                    IsActive = true,
                    SortOrder = 0,
                    DateCreated = DateTime.Now
                };
                if (!db.Extensions.AsNoTracking().Any(it => it.Code.Equals(xlsx.Code)))
                    db.Extensions.Add(xlsx);

                var xls = new Extension
                {
                    Code = "XLS",
                    Name = "xls",
                    IsActive = true,
                    SortOrder = 0,
                    DateCreated = DateTime.Now
                };
                if (!db.Extensions.AsNoTracking().Any(it => it.Code.Equals(xls.Code)))
                    db.Extensions.Add(xls);

                var pptx = new Extension
                {
                    Code = "PPTX",
                    Name = "pptx",
                    IsActive = true,
                    SortOrder = 0,
                    DateCreated = DateTime.Now
                };
                if (!db.Extensions.AsNoTracking().Any(it => it.Code.Equals(pptx.Code)))
                    db.Extensions.Add(pptx);

                var mp3 = new Extension
                {
                    Code = "MP3",
                    Name = "mp3",
                    IsActive = true,
                    SortOrder = 0,
                    DateCreated = DateTime.Now
                };
                if (!db.Extensions.AsNoTracking().Any(it => it.Code.Equals(mp3.Code)))
                    db.Extensions.Add(mp3);

                var mp4 = new Extension
                {
                    Code = "MP4",
                    Name = "mp4",
                    IsActive = true,
                    SortOrder = 0,
                    DateCreated = DateTime.Now
                };
                if (!db.Extensions.AsNoTracking().Any(it => it.Code.Equals(mp4.Code)))
                    db.Extensions.Add(mp4);

                var mkv = new Extension
                {
                    Code = "MKV",
                    Name = "mkv",
                    IsActive = true,
                    SortOrder = 0,
                    DateCreated = DateTime.Now
                };
                if (!db.Extensions.AsNoTracking().Any(it => it.Code.Equals(mkv.Code)))
                    db.Extensions.Add(mkv);
                var ogg = new Extension
                {
                    Code = "OGG",
                    Name = "ogg",
                    IsActive = true,
                    SortOrder = 0,
                    DateCreated = DateTime.Now
                };
                if (!db.Extensions.AsNoTracking().Any(it => it.Code.Equals(ogg.Code)))
                    db.Extensions.Add(ogg);


                var read = new Permission
                {
                    Code = "READ",
                    Name = "Read",
                    DateCreated = DateTime.Now,
                    IsActive = true,
                    SortOrder = 1
                };
                if (!db.Permissions.AsNoTracking().Any(it => it.Code.Equals(read.Code)))
                    db.Permissions.Add(read);

                var write = new Permission
                {
                    Code = "WRITE",
                    Name = "Write",
                    DateCreated = DateTime.Now,
                    IsActive = true,
                    SortOrder = 8
                };
                if (!db.Permissions.AsNoTracking().Any(it => it.Code.Equals(write.Code)))
                    db.Permissions.Add(write);

                var edit = new Permission
                {
                    Code = "EDIT",
                    Name = "Edit",
                    DateCreated = DateTime.Now,
                    IsActive = true,
                    SortOrder = 4
                };
                if (!db.Permissions.AsNoTracking().Any(it => it.Code.Equals(edit.Code)))
                    db.Permissions.Add(edit);

                var create = new Permission
                {
                    Code = "CREATE",
                    Name = "Create",
                    DateCreated = DateTime.Now,
                    IsActive = true,
                    SortOrder = 2
                };
                if (!db.Permissions.AsNoTracking().Any(it => it.Code.Equals(create.Code)))
                    db.Permissions.Add(create);

                var delete = new Permission
                {
                    Code = "DELETE",
                    Name = "Delete",
                    DateCreated = DateTime.Now,
                    IsActive = true,
                    SortOrder = 8
                };
                if (!db.Permissions.AsNoTracking().Any(it => it.Code.Equals(delete.Code)))
                    db.Permissions.Add(delete);

                var videoAttachmentType = new AttachmentType
                {
                    Code = "VIDEOATTACHMENT",
                    Name = "Video Attachment",
                    DateCreated = DateTime.Now,
                    IsActive = true,
                    SortOrder = 0
                };

                if (!db.AttachmentTypes.AsNoTracking().Any(it => it.Code.Equals(videoAttachmentType.Code)))
                    db.AttachmentTypes.Add(videoAttachmentType);

                var audioAttachmentType = new AttachmentType
                {
                    Code = "AUDIOATTACHMENT",
                    Name = "Audio Attachment",
                    DateCreated = DateTime.Now,
                    IsActive = true,
                    SortOrder = 0
                };
                if (!db.AttachmentTypes.AsNoTracking().Any(it => it.Code.Equals(audioAttachmentType.Code)))
                    db.AttachmentTypes.Add(audioAttachmentType);

                var imageAttachmentType = new AttachmentType
                {
                    Code = "IMAGEATTACHMENT",
                    Name = "Image Attachment",
                    DateCreated = DateTime.Now,
                    IsActive = true,
                    SortOrder = 0
                };
                if (!db.AttachmentTypes.AsNoTracking().Any(it => it.Code.Equals(imageAttachmentType.Code)))
                    db.AttachmentTypes.Add(imageAttachmentType);

                var passportAttachmentType = new AttachmentType
                {
                    Code = "PASSPORT",
                    Name = "Passport",
                    DateCreated = DateTime.Now,
                    IsActive = true,
                    SortOrder = 0
                };
                if (!db.AttachmentTypes.AsNoTracking().Any(it => it.Code.Equals(passportAttachmentType.Code)))
                    db.AttachmentTypes.Add(passportAttachmentType);
                //passport extensions cfg
                if (!db.AttachmentTypeExtentions.AsNoTracking().Include(it => it.AttachmentType).Include(it => it.Extension).Any(it => it.AttachmentType.Code == passportAttachmentType.Code && jpeg.Code == it.Extension.Code))
                {
                    var test1 = new AttachmentTypeExtension
                    {
                        //Extension=jpeg,
                        //AttachmentType=passportAttachmentType
                        ExtensionId = jpeg.Id,
                        AttachmentTypeId = passportAttachmentType.Id
                    };
                    db.AttachmentTypeExtentions.Add(test1);
                }
                db.SaveChanges();
                if (!db.AttachmentTypeExtentions.AsNoTracking().Include(it => it.AttachmentType).Include(it => it.Extension).Any(it => it.AttachmentType.Code == passportAttachmentType.Code && jpg.Code == it.Extension.Code))
                {
                    var test2 = new AttachmentTypeExtension
                    {
                        //Extension = jpg,
                        //AttachmentType = passportAttachmentType
                        ExtensionId = jpg.Id,
                        AttachmentTypeId = passportAttachmentType.Id
                    };
                    db.AttachmentTypeExtentions.Add(test2);
                }
                db.SaveChanges();
                if (!db.AttachmentTypeExtentions.Include(it => it.AttachmentType).Include(it => it.Extension).Any(it => it.AttachmentType.Code == passportAttachmentType.Code && tiff.Code == it.Extension.Code))
                {
                    db.AttachmentTypeExtentions.Add(new AttachmentTypeExtension
                    {
                        ExtensionId = tiff.Id,
                        AttachmentTypeId = passportAttachmentType.Id
                    });
                }
                if (!db.AttachmentTypeExtentions.Include(it => it.AttachmentType).Include(it => it.Extension).Any(it => it.AttachmentType.Code == passportAttachmentType.Code && bmp.Code == it.Extension.Code))
                {
                    db.AttachmentTypeExtentions.Add(new AttachmentTypeExtension
                    {
                        ExtensionId = bmp.Id,
                        AttachmentTypeId = passportAttachmentType.Id
                    });
                }
                //passport extensions cfg end
                //video extensions cfg
                if (!db.AttachmentTypeExtentions.Include(it => it.AttachmentType).Include(it => it.Extension).Any(it => it.AttachmentType.Code == videoAttachmentType.Code && mkv.Code == it.Extension.Code))
                {
                    db.AttachmentTypeExtentions.Add(new AttachmentTypeExtension
                    {
                        ExtensionId = mkv.Id,
                        AttachmentTypeId = videoAttachmentType.Id
                    });
                }

                if (!db.AttachmentTypeExtentions.Include(it => it.AttachmentType).Include(it => it.Extension).Any(it => it.AttachmentType.Code == videoAttachmentType.Code && ogg.Code == it.Extension.Code))
                {
                    db.AttachmentTypeExtentions.Add(new AttachmentTypeExtension
                    {
                        ExtensionId = ogg.Id,
                        AttachmentTypeId = videoAttachmentType.Id
                    });
                }
                //video extensions cfg ended

                //audio extensions cfg
                if (!db.AttachmentTypeExtentions.Include(it => it.AttachmentType).Include(it => it.Extension).Any(it => it.AttachmentType.Code == audioAttachmentType.Code && mp3.Code == it.Extension.Code))
                {
                    db.AttachmentTypeExtentions.Add(new AttachmentTypeExtension
                    {
                        ExtensionId = mp3.Id,
                        AttachmentTypeId = audioAttachmentType.Id
                    });
                }

                if (!db.AttachmentTypeExtentions.Include(it => it.AttachmentType).Include(it => it.Extension).Any(it => it.AttachmentType.Code == audioAttachmentType.Code && mp4.Code == it.Extension.Code))
                {
                    db.AttachmentTypeExtentions.Add(new AttachmentTypeExtension
                    {
                        ExtensionId = mp4.Id,
                        AttachmentTypeId = audioAttachmentType.Id
                    });
                }
                //audio extensions cfg ended
                //images extensions cfg
                if (!db.AttachmentTypeExtentions.Include(it => it.AttachmentType).Include(it => it.Extension).Any(it => it.AttachmentType.Code == imageAttachmentType.Code && tiff.Code == it.Extension.Code))
                {
                    db.AttachmentTypeExtentions.Add(new AttachmentTypeExtension
                    {
                        ExtensionId = tiff.Id,
                        AttachmentTypeId = imageAttachmentType.Id
                    });
                }

                if (!db.AttachmentTypeExtentions.Include(it => it.AttachmentType).Include(it => it.Extension).Any(it => it.AttachmentType.Code == imageAttachmentType.Code && jpeg.Code == it.Extension.Code))
                {
                    db.AttachmentTypeExtentions.Add(new AttachmentTypeExtension
                    {
                        ExtensionId = jpeg.Id,
                        AttachmentTypeId = imageAttachmentType.Id
                    });
                }

                if (!db.AttachmentTypeExtentions.Include(it => it.AttachmentType).Include(it => it.Extension).Any(it => it.AttachmentType.Code == imageAttachmentType.Code && jpg.Code == it.Extension.Code))
                {
                    db.AttachmentTypeExtentions.Add(new AttachmentTypeExtension
                    {
                        ExtensionId = jpg.Id,
                        AttachmentTypeId = imageAttachmentType.Id
                    });
                }

                if (!db.AttachmentTypeExtentions.Include(it => it.AttachmentType).Include(it => it.Extension).Any(it => it.AttachmentType.Code == imageAttachmentType.Code && png.Code == it.Extension.Code))
                {
                    db.AttachmentTypeExtentions.Add(new AttachmentTypeExtension
                    {
                        ExtensionId = png.Id,
                        AttachmentTypeId = imageAttachmentType.Id
                    });
                }

                if (!db.AttachmentTypeExtentions.Include(it => it.AttachmentType).Include(it => it.Extension).Any(it => it.AttachmentType.Code == imageAttachmentType.Code && gif.Code == it.Extension.Code))
                {
                    db.AttachmentTypeExtentions.Add(new AttachmentTypeExtension
                    {
                        ExtensionId = gif.Id,
                        AttachmentTypeId = imageAttachmentType.Id
                    });
                }
                if (!db.AttachmentTypePermissions.Include(it => it.AttachmentType).Include(it => it.Permission)
                    .Any(it => it.AttachmentType.Code == passportAttachmentType.Code && read.Code == it.Permission.Code
                    && it.Role.Name == userRole.Name))
                {
                    db.AttachmentTypePermissions.Add(new AttachmentTypePermission
                    {
                        RoleId = userRole.Id,
                        AttachmentTypeId = passportAttachmentType.Id,
                        PermissionId = read.Id
                    });
                }
                if (!db.AttachmentTypePermissions.Include(it => it.AttachmentType).Include(it => it.Permission)
                    .Any(it => it.AttachmentType.Code == passportAttachmentType.Code && read.Code == it.Permission.Code
                    && it.Role.Name == crafter.Name))
                {
                    db.AttachmentTypePermissions.Add(new AttachmentTypePermission
                    {
                        RoleId = crafter.Id,
                        AttachmentTypeId = passportAttachmentType.Id,
                        PermissionId = read.Id
                    });
                }

                if (!db.AttachmentTypePermissions.Include(it => it.AttachmentType).Include(it => it.Permission)
                    .Any(it => it.AttachmentType.Code == videoAttachmentType.Code && read.Code == it.Permission.Code
                    && it.Role.Name == userRole.Name))
                {
                    db.AttachmentTypePermissions.Add(new AttachmentTypePermission
                    {
                        RoleId = userRole.Id,
                        AttachmentTypeId = videoAttachmentType.Id,
                        PermissionId = read.Id
                    });
                }
                if (!db.AttachmentTypePermissions.Include(it => it.AttachmentType).Include(it => it.Permission)
                    .Any(it => it.AttachmentType.Code == videoAttachmentType.Code && read.Code == it.Permission.Code
                    && it.Role.Name == crafter.Name))
                {
                    db.AttachmentTypePermissions.Add(new AttachmentTypePermission
                    {
                        RoleId = crafter.Id,
                        AttachmentTypeId = videoAttachmentType.Id,
                        PermissionId = read.Id
                    });
                }

                if (!db.AttachmentTypePermissions.Include(it => it.AttachmentType).Include(it => it.Permission)
                    .Any(it => it.AttachmentType.Code == audioAttachmentType.Code && read.Code == it.Permission.Code
                    && it.Role.Name == userRole.Name))
                {
                    db.AttachmentTypePermissions.Add(new AttachmentTypePermission
                    {
                        RoleId = userRole.Id,
                        AttachmentTypeId = audioAttachmentType.Id,
                        PermissionId = read.Id
                    });
                }
                if (!db.AttachmentTypePermissions.Include(it => it.AttachmentType).Include(it => it.Permission)
                    .Any(it => it.AttachmentType.Code == audioAttachmentType.Code && read.Code == it.Permission.Code
                    && it.Role.Name == crafter.Name))
                {
                    db.AttachmentTypePermissions.Add(new AttachmentTypePermission
                    {
                        RoleId = crafter.Id,
                        AttachmentTypeId = audioAttachmentType.Id,
                        PermissionId = read.Id
                    });
                }

                if (!db.AttachmentTypePermissions.Include(it => it.AttachmentType).Include(it => it.Permission)
                    .Any(it => it.AttachmentType.Code == videoAttachmentType.Code && read.Code == it.Permission.Code
                    && it.Role.Name == userRole.Name))
                {
                    db.AttachmentTypePermissions.Add(new AttachmentTypePermission
                    {
                        RoleId = userRole.Id,
                        AttachmentTypeId = imageAttachmentType.Id,
                        PermissionId = read.Id
                    });
                }
                if (!db.AttachmentTypePermissions.Include(it => it.AttachmentType).Include(it => it.Permission)
                    .Any(it => it.AttachmentType.Code == videoAttachmentType.Code && read.Code == it.Permission.Code
                    && it.Role.Name == crafter.Name))
                {
                    db.AttachmentTypePermissions.Add(new AttachmentTypePermission
                    {
                        RoleId = crafter.Id,
                        AttachmentTypeId = imageAttachmentType.Id,
                        PermissionId = read.Id
                    });
                }

                var testAttachment = new AttachmentN
                {
                    Name = "Test2",
                    Code = "TEST2",
                    IsActive = true,
                    DateCreated = DateTime.Now,
                    SortOrder = 0,
                    ExtensionId = jpeg.Id,
                    PublicUri = string.Empty,
                    IsShared = false,
                    Content = new byte[] { 1, 2, 3, 4, 5, 7, 9 },
                    AttachmentTypeId = passportAttachmentType.Id
                };
                db.Attachments.Add(testAttachment);

                var testUser = new User
                {
                    UserName = "Andrew",
                    Email = "Andrew",
                    FirstName = String.Empty,
                    LastName = String.Empty,
                    Patronymic = String.Empty
                };
                db.Set<User>().Add(testUser);
                db.AttachmentPermissions.Add(new AttachmentPermission
                {
                    AttachmentId = testAttachment.Id,
                    IsOwner = true,
                    PermissionId = read.Id,
                    UserId = testUser.Id
                });
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                LogEventManager.Logger.Error(ex.Message,ex);
                throw;
            }
            
         

        }
    }
}