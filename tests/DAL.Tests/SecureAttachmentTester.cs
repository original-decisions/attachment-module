using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NUnit.Framework;
using odec.Attachment.DAL;
using odec.Framework.Logging;
using odec.Server.Model.Attachment;
using odec.Server.Model.Attachment.Extended;
using odec.Server.Model.Attachment.Filters;
using odec.Server.Model.Secure.Attachment;
using odec.Server.Model.Secure.Attachment.Contexts;
using odec.Server.Model.Secure.Attachment.Specific.Filters;
using AttachmentN = odec.Server.Model.Attachment.Attachment;
//using ISecureRepo = odec.Attachment.DAL.Interop.ISecureAttachmentRepository<System.Data.Entity.DbContext, int, odec.Server.Model.Attachment.Attachment, odec.Server.Model.Attachment.Specific.Filters.AttachmentFilter<int?>, odec.Server.Model.Secure.Attachment.Permission, odec.Server.Model.Secure.Attachment.Specific.Filters.AttachmentPermissionFilter<int, int?, int?>, odec.Server.Model.User.User>;
namespace Attachment.DAL.Tests
{
    class SecureAttachmentTester : Tester<SecureAttachmentContext>
    {
        public SecureAttachmentTester() { }

        [Test]

        public void Get_Attachments()
        {
            try
            {
                var options = CreateNewContextOptions();
                using (var db = new SecureAttachmentContext(options))
                {
                    //var repository = IocHelper.GetObject<ISecureRepo>(db);

                    AttachmentTestHelper.PopulateSecurityAttachmentCtx(db);

                }
                using (var db = new SecureAttachmentContext(options))
                {
                    var repository = new SecureAttachmentRepository(db);
                    IEnumerable<AttachmentN> result = null;
                    var passPortAT = db.Set<AttachmentType>().Single(it => it.Code == "PASSPORT");
                    var extention = db.Set<Extension>().Single(it => it.Code == "JPEG");
                    Assert.DoesNotThrow(() => result = repository.Get(new AttachmentFilter<int?>
                    {
                        ExtensionId = extention.Id,
                        IsOnlyActive = true,
                        AttachmentTypeId = passPortAT.Id
                    }));
                    //Удаляем созданный объект
                    Assert.NotNull(result);
                    //проверяем, что он сохранился(присвоился новый идентификатор в базе)
                    Assert.Greater(result.Count(), 0);
                }
            }
            catch (Exception ex)
            {
                LogEventManager.Logger.Error(ex);
                throw;
            }
        }
        [Test]
        public void GetAttachmentPermissions()
        {
            try
            {
                var options = CreateNewContextOptions();
                using (var db = new SecureAttachmentContext(options))
                {
                    AttachmentTestHelper.PopulateSecurityAttachmentCtx(db);
                }
                //var repository = IocHelper.GetObject<ISecureRepo>(db);
                using (var db = new SecureAttachmentContext(options))
                {
                    var repository = new SecureAttachmentRepository(db);

                    var item = db.Attachments.First();
                    var user = db.Users.First();
                    IEnumerable<odec.Server.Model.Secure.Attachment.Permission> permissions = null;
                    Assert.DoesNotThrow(
                        () =>
                            permissions =
                                repository.GetAttachmentPermissions(new AttachmentPermissionFilter<int, int?, int?>
                                {
                                    UserId = user.Id,
                                    IsOnlyActive = false,

                                    AttachmentId = item.Id
                                }));

                    Assert.NotNull(permissions);
                    Assert.Greater(permissions.Count(), 0);
                }

            }
            catch (Exception ex)
            {
                LogEventManager.Logger.Error(ex);
                throw;
            }
        }

        [Test]
        public void LinkAttachment()
        {

            try
            {
                var options = CreateNewContextOptions();
                using (var db = new SecureAttachmentContext(options))
                {
                    AttachmentTestHelper.PopulateSecurityAttachmentCtx(db);
                }
                using (var db = new SecureAttachmentContext(options))
                {
                    var repository = new SecureAttachmentRepository(db);

                    var item = db.Attachments.First();
                    var user = db.Users.First();
                    var result = db.Set<AttachmentPermission>().ToList();
                    ICollection<Permission> permissions = db.Set<Permission>().Take(4).ToList();
                    db.Set<AttachmentPermission>().RemoveRange(result);
                    db.SaveChanges();
                    Assert.DoesNotThrow(() => repository.LinkAttachment(user, item));
                    result = db.Set<AttachmentPermission>().Where(it => it.AttachmentId == item.Id && it.UserId == user.Id && it.IsOwner).ToList();
                    Assert.NotNull(result);
                    Assert.Greater(result.Count, 0);
                    db.Set<AttachmentPermission>().RemoveRange(result);
                    db.SaveChanges();
                    Assert.DoesNotThrow(() => repository.LinkAttachment(user, item, permissions));
                    result = db.Set<AttachmentPermission>().Where(it => it.AttachmentId == item.Id && it.UserId == user.Id).ToList();
                    Assert.NotNull(result);
                    Assert.True(result.Count == 4);


                    Assert.NotNull(permissions);
                    Assert.Greater(permissions.Count(), 0);
                }
                //var repository = IocHelper.GetObject<ISecureRepo>(db);


            }
            catch (Exception ex)
            {
                LogEventManager.Logger.Error(ex);
                throw;
            }
        }
        [Test]
        public void RemoveAttachment()
        {
            try
            {
                var options = CreateNewContextOptions();
                using (var db = new SecureAttachmentContext(options))
                {
                    AttachmentTestHelper.PopulateSecurityAttachmentCtx(db);
                }
                using (var db = new SecureAttachmentContext(options))
                {
                    var repository = new SecureAttachmentRepository(db);
                    var item = db.Attachments.First();
                    var user = db.Users.First();
                    var result = db.Set<AttachmentPermission>().ToList();
                    ICollection<Permission> permissions = db.Set<Permission>().Take(4).ToList();
                    db.Set<AttachmentPermission>().RemoveRange(result);
                    db.SaveChanges();
                    Assert.DoesNotThrow(() => repository.LinkAttachment(user, item));
                    result = db.Set<AttachmentPermission>().Where(it => it.AttachmentId == item.Id && it.UserId == user.Id && it.IsOwner).ToList();
                    Assert.NotNull(result);
                    Assert.Greater(result.Count, 0);
                    Assert.DoesNotThrow(() => repository.RemoveAttachment(user, item));
                    result = db.Set<AttachmentPermission>().Where(it => it.AttachmentId == item.Id && it.UserId == user.Id).ToList();
                    Assert.True(result.Count == 0);
                    Assert.DoesNotThrow(() => repository.LinkAttachment(user, item, permissions));
                    Assert.DoesNotThrow(() => repository.RemoveAttachment(user, item, new List<Permission> { permissions.First() }));
                    result = db.Set<AttachmentPermission>().Where(it => it.AttachmentId == item.Id && it.UserId == user.Id).ToList();
                    Assert.NotNull(result);
                    Assert.True(result.Count == 3);


                    Assert.NotNull(permissions);
                    Assert.Greater(permissions.Count(), 0);
                }
                //var repository = IocHelper.GetObject<ISecureRepo>(db);


            }
            catch (Exception ex)
            {
                LogEventManager.Logger.Error(ex);
                throw;
            }
        }

        private AttachmentN GenerateModel()
        {
            return new AttachmentN
            {
                Name = "Test",
                Code = "TEST",
                IsActive = true,
                DateCreated = DateTime.Now,
                SortOrder = 0,
                ExtensionId = 1,
                PublicUri = string.Empty,
                IsShared = false,
                Content = new byte[] { 1, 2, 3, 5, 5, 5, 6, 6, 8, 9 },
                AttachmentType = new AttachmentType
                {
                    Name = "Test",
                    Code = "TEST",
                    IsActive = true,
                    DateCreated = DateTime.Now,
                    SortOrder = 0
                }
            };
        }


        [Test]
        public void Save()
        {
            try
            {
                var options = CreateNewContextOptions();
                using (var db = new SecureAttachmentContext(options))
                {
                    //var repository = IocHelper.GetObject<ISecureRepo>(db);
                    AttachmentTestHelper.PopulateSecurityAttachmentCtx(db);
                    //сохраняем генерированный объект

                }
                using (var db = new SecureAttachmentContext(options))
                {
                    //var repository = IocHelper.GetObject<ISecureRepo>(db);
                    var repository = new SecureAttachmentRepository(db);

                    var item = GenerateModel();
                    //сохраняем генерированный объект
                    Assert.DoesNotThrow(() => repository.Save(item));
                    //Удаляем созданный объект
                    Assert.DoesNotThrow(() => repository.Delete(item));
                    //проверяем, что он сохранился(присвоился новый идентификатор в базе)
                    Assert.Greater(item.Id, 0);
                }

            }
            catch (Exception ex)
            {
                LogEventManager.Logger.Error(ex);
                throw;
            }
        }

        [Test]
        public void Delete()
        {
            try
            {
                var options = CreateNewContextOptions();
                using (var db = new SecureAttachmentContext(options))
                {
                    //var repository = IocHelper.GetObject<ISecureRepo>(db);
                    var repository = new SecureAttachmentRepository(db);
                    // AttachmentTestHelper.PopulateSecurityAttachmentCtx(db);
                    var item = GenerateModel();
                    //сохраняем генерированный объект
                    Assert.DoesNotThrow(() => repository.Save(item));
                    //Удаляем созданный объект
                    Assert.DoesNotThrow(() => repository.Delete(item));
                }

            }
            catch (Exception ex)
            {
                LogEventManager.Logger.Error(ex);
                throw;
            }
        }

        [Test]
        public void DeleteById()
        {
            try
            {
                var options = CreateNewContextOptions();
                using (var db = new SecureAttachmentContext(options))
                {
                    //var repository = IocHelper.GetObject<ISecureRepo>(db);
                    var repository = new SecureAttachmentRepository(db);
                    //     AttachmentTestHelper.PopulateSecurityAttachmentCtx(db);
                    var item = GenerateModel();
                    //сохраняем генерированный объект
                    Assert.DoesNotThrow(() => repository.Save(item));
                    //Удаляем созданный объект
                    Assert.DoesNotThrow(() => repository.Delete(item.Id));
                }

            }
            catch (Exception ex)
            {
                LogEventManager.Logger.Error(ex);
                throw;
            }
        }

        [Test]
        public void Deactivate()
        {
            try
            {
                var options = CreateNewContextOptions();
                using (var db = new SecureAttachmentContext(options))
                {
                    AttachmentTestHelper.PopulateSecurityAttachmentCtx(db);
                }
                //var repository = IocHelper.GetObject<ISecureRepo>(db);
                using (var db = new SecureAttachmentContext(options))
                {
                    var repository = new SecureAttachmentRepository(db);

                    var item = GenerateModel();
                    item.IsActive = true;
                    //сохраняем генерированный объект
                    Assert.DoesNotThrow(() => repository.Save(item));
                    //вызов деактивации серверного объекта
                    Assert.DoesNotThrow(() => repository.Deactivate(item));
                    //Удаляем созданный объект
                    Assert.DoesNotThrow(() => repository.Delete(item));
                    //Проверка, что деактивация сработала
                    Assert.IsFalse(item.IsActive);
                }

            }
            catch (Exception ex)
            {
                LogEventManager.Logger.Error(ex);
                throw;
            }
        }

        [Test]
        public void DeactivateById()
        {
            try
            {
                var options = CreateNewContextOptions();
                using (var db = new SecureAttachmentContext(options))
                {
                    AttachmentTestHelper.PopulateSecurityAttachmentCtx(db);
                }
                //var repository = IocHelper.GetObject<ISecureRepo>(db);


                using (var db = new SecureAttachmentContext(options))
                {
                    var repository = new SecureAttachmentRepository(db);
                    var item = GenerateModel();
                    item.IsActive = true;
                    //сохраняем генерированный объект
                    Assert.DoesNotThrow(() => repository.Save(item));
                    //вызов деактивации серверного объекта
                    Assert.DoesNotThrow(() => item = repository.Deactivate(item.Id));
                    //Удаляем созданный объект
                    Assert.DoesNotThrow(() => repository.Delete(item));
                    //Проверка, что деактивация сработала
                    Assert.IsFalse(item.IsActive);
                }

            }
            catch (Exception ex)
            {
                LogEventManager.Logger.Error(ex);
                throw;
            }
        }

        [Test]
        public void Activate()
        {
            try
            {
                var options = CreateNewContextOptions();
                using (var db = new SecureAttachmentContext(options))
                {
                    AttachmentTestHelper.PopulateSecurityAttachmentCtx(db);
                }
                using (var db = new SecureAttachmentContext(options))
                {//var repository = IocHelper.GetObject<ISecureRepo>(db);
                    var repository = new SecureAttachmentRepository(db);

                    var item = GenerateModel();
                    item.IsActive = false;
                    //сохраняем генерированный объект
                    Assert.DoesNotThrow(() => repository.Save(item));
                    //вызов активации серверного объекта
                    Assert.DoesNotThrow(() => repository.Activate(item));
                    //Удаляем созданный объект
                    Assert.DoesNotThrow(() => repository.Delete(item));
                    //Проверка, что активация сработала
                    Assert.IsTrue(item.IsActive);
                }

            }
            catch (Exception ex)
            {
                LogEventManager.Logger.Error(ex);
                throw;
            }
        }

        [Test]
        public void ActivateById()
        {
            try
            {
                var options = CreateNewContextOptions();
                using (var db = new SecureAttachmentContext(options))
                {
                    AttachmentTestHelper.PopulateSecurityAttachmentCtx(db);
                }
                using (var db = new SecureAttachmentContext(options))
                {//var repository = IocHelper.GetObject<ISecureRepo>(db);
                    var repository = new SecureAttachmentRepository(db);

                    var item = GenerateModel();
                    item.IsActive = false;
                    //сохраняем генерированный объект
                    Assert.DoesNotThrow(() => repository.Save(item));
                    //вызов активации серверного объекта
                    Assert.DoesNotThrow(() => item = repository.Activate(item.Id));
                    //Удаляем созданный объект
                    Assert.DoesNotThrow(() => repository.Delete(item));
                    //Проверка, что активация сработала
                    Assert.IsTrue(item.IsActive);
                }

            }
            catch (Exception ex)
            {
                LogEventManager.Logger.Error(ex);
                throw;
            }
        }

        [Test]
        public void GetById()
        {
            try
            {
                var options = CreateNewContextOptions();
                using (var db = new SecureAttachmentContext(options))
                {
                    AttachmentTestHelper.PopulateSecurityAttachmentCtx(db);
                }
                using (var db = new SecureAttachmentContext(options))
                { //var repository = IocHelper.GetObject<ISecureRepo>(db);
                    var repository = new SecureAttachmentRepository(db);

                    var item = GenerateModel();
                    Assert.DoesNotThrow(() => repository.Save(item));

                    Assert.DoesNotThrow(() => item = repository.GetById(item.Id));
                    Assert.DoesNotThrow(() => repository.Delete(item));
                    Assert.NotNull(item);
                    Assert.Greater(item.Id, 0);
                }


            }
            catch (Exception ex)
            {
                LogEventManager.Logger.Error(ex);
                throw;
            }
        }
    }
}
