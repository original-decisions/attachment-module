using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using odec.Attachment.DAL;
using odec.Framework.Logging;
using odec.Server.Model.Attachment;
using odec.Server.Model.Attachment.Extended;
using odec.Server.Model.Attachment.Filters;
using odec.Server.Model.Secure.Attachment.Contexts;
using odec.Server.Model.Secure.Attachment.Specific.Filters;
//using ISecureRepo = odec.Attachment.DAL.Interop.ISecureAttachmentTypeRepository<System.Data.Entity.DbContext,
//    int, odec.Server.Model.Attachment.AttachmentType, odec.Server.Model.Attachment.Specific.Filters.AttachmentTypeFilter<int>,
//    odec.Server.Model.Attachment.Extended.Extension, odec.Server.Model.Attachment.Attachment, odec.Server.Model.Secure.Attachment.Permission, odec.Server.Model.Secure.Attachment.Specific.Filters.AttachmentTypePermissionFilter<int?>>;


namespace Attachment.DAL.Tests
{
    class SecureAttachmentTypeTester : Tester<SecureAttachmentContext>
    {

        [Test]
        public void GetAttachmentTypes()
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
                    var repository = new SecureAttachmentTypeRepository(db);
                    IEnumerable<AttachmentType> result = null;
                    Assert.DoesNotThrow(() => result = repository.Get(new AttachmentTypeFilter<int>
                    {
                        IsOnlyActive = false,
                        Name = "Passport",
                        Ids = new List<int>()
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
        public void GetAttachmentTypeExtentions()
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
                    var repository = new SecureAttachmentTypeRepository(db);
                    var item = db.AttachmentTypes.First();
                    IEnumerable<Extension> result = null;
                    Assert.DoesNotThrow(() => result = repository.GetAttachmentTypeExtentions(item.Id));

                    Assert.NotNull(result);
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
                    //var repository = IocHelper.GetObject<ISecureRepo>(db);

                    AttachmentTestHelper.PopulateSecurityAttachmentCtx(db);
                }
                using (var db = new SecureAttachmentContext(options))
                {
                    var repository = new SecureAttachmentTypeRepository(db);
                    var item = db.AttachmentTypes.First();
                    var role = db.Roles.First();
                    IEnumerable<odec.Server.Model.Secure.Attachment.Permission> permissions = null;
                    Assert.DoesNotThrow(
                        () =>
                            permissions =
                                repository.GetAttachmentTypePermissions(new AttachmentTypePermissionFilter<int?>
                                {
                                    AttachmentTypeId = item.Id,
                                    RoleId = role.Id,
                                    IsOnlyActive = false
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


        private AttachmentType GenerateModel()
        {
            return new AttachmentType
            {
                Name = "Test",
                Code = "TEST",
                IsActive = true,
                DateCreated = DateTime.Now,
                SortOrder = 0
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
                }
                using (var db = new SecureAttachmentContext(options))
                {
                    var repository = new SecureAttachmentTypeRepository(db);
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
                    AttachmentTestHelper.PopulateSecurityAttachmentCtx(db);
                }
                using (var db = new SecureAttachmentContext(options))
                {
                    var repository = new SecureAttachmentTypeRepository(db);
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

                    AttachmentTestHelper.PopulateSecurityAttachmentCtx(db);

                }
                using (var db = new SecureAttachmentContext(options))
                {
                    var repository = new SecureAttachmentTypeRepository(db);
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
                    //var repository = IocHelper.GetObject<ISecureRepo>(db);

                    AttachmentTestHelper.PopulateSecurityAttachmentCtx(db);

                }
                using (var db = new SecureAttachmentContext(options))
                {
                    var repository = new SecureAttachmentTypeRepository(db);
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
                    //var repository = IocHelper.GetObject<ISecureRepo>(db);

                    AttachmentTestHelper.PopulateSecurityAttachmentCtx(db);
                }
                using (var db = new SecureAttachmentContext(options))
                {
                    var repository = new SecureAttachmentTypeRepository(db);
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
                    //var repository = IocHelper.GetObject<ISecureRepo>(db);

                    AttachmentTestHelper.PopulateSecurityAttachmentCtx(db);

                }
                using (var db = new SecureAttachmentContext(options))
                {
                    var repository = new SecureAttachmentTypeRepository(db);
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
                    //var repository = IocHelper.GetObject<ISecureRepo>(db);
                    AttachmentTestHelper.PopulateSecurityAttachmentCtx(db);

                }
                using (var db = new SecureAttachmentContext(options))
                {
                    //var repository = IocHelper.GetObject<ISecureRepo>(db);

                    var repository = new SecureAttachmentTypeRepository(db);
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
                    //var repository = IocHelper.GetObject<ISecureRepo>(db);

                    AttachmentTestHelper.PopulateSecurityAttachmentCtx(db);
                }
                using (var db = new SecureAttachmentContext(options))
                {
                    var repository = new SecureAttachmentTypeRepository(db);
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
