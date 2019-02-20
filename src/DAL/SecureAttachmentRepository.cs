using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using odec.Attachment.DAL.Interop;
using odec.Framework.Logging;
using odec.Server.Model.Attachment.Filters;
using odec.Server.Model.Secure.Attachment;
using odec.Server.Model.Secure.Attachment.Specific.Filters;
using odec.Server.Model.User;

namespace odec.Attachment.DAL
{
    public class SecureAttachmentRepository : AttachmentRepository,
        ISecureAttachmentRepository<DbContext, int, Server.Model.Attachment.Attachment, AttachmentFilter<int?>, Permission, AttachmentPermissionFilter<int,int?,int?>,User>
    {
        public SecureAttachmentRepository()
        {
            
        }
        public SecureAttachmentRepository(DbContext db)
        {
            Db = db;
        }

        public override IEnumerable<Server.Model.Attachment.Attachment> Get(AttachmentFilter<int?> filter)
        {
            try
            {
                var realFilter = filter as SecuredAttachmentFilter<int?, int?, int?>;
                if (realFilter == null)
                    return base.Get(filter);
                return (from attach in base.Get(filter)
                        join attachmentTypePermission in Db.Set<AttachmentTypePermission>() on attach.AttachmentTypeId equals
                            attachmentTypePermission.AttachmentTypeId
                        where
                            (realFilter.RoleIds==null || !realFilter.RoleIds.Any() || realFilter.RoleIds.Contains(attachmentTypePermission.RoleId)) &&
                            (realFilter.PermissionIds == null || !realFilter.PermissionIds.Any() || realFilter.PermissionIds.Contains(attachmentTypePermission.PermissionId))
                        select attach).Distinct();
            }
            catch (Exception ex)
            {
                LogEventManager.Logger.Error(ex.Message,ex);
                throw;
            }
        }

        public virtual IEnumerable<Permission> GetAttachmentPermissions(AttachmentPermissionFilter<int,int?,int?> filter)
        {
            try
            {
                var res = (from attachmentPermission in Db.Set<AttachmentPermission>()
                    join attachment in Db.Set<Server.Model.Attachment.Attachment>() on attachmentPermission.AttachmentId equals attachment.Id
                    join permission in Db.Set<Permission>() on attachmentPermission.PermissionId equals
                        permission.Id
                    where (!filter.IsOnlyActive || (filter.IsOnlyActive && permission.IsActive)) 
                    && (!filter.RoleId.HasValue || filter.RoleId == attachmentPermission.UserId)
                    //&& (!filter.RoleId.HasValue || filter.RoleId == attachmentPermission.RoleId)
                    && filter.AttachmentId == attachment.Id
                        select permission).Distinct().ToList();
                return res;

            }
            catch (Exception ex)
            {
                LogEventManager.Logger.Error(ex.Message,ex);
                throw;
            }

        }
        /// <summary>
        /// Links an attachment to entity
        /// </summary>
        /// <param name="entity">entity(user)</param>
        /// <param name="attachment">attachment</param>
        /// <param name="permissions">Permissions for attachment. If null provided - then we set full access and mark entity as owner</param>
        public void LinkAttachment(User entity, Server.Model.Attachment.Attachment attachment, ICollection<Permission> permissions = null)
        {
            //using (var tran = Db.Database.BeginTransaction())
            //{
                try
                {
                    var isOwner = false;
                    if (!Db.Set<User>().Any(it => it.Id == entity.Id))
                        throw new ArgumentException("Cant link. User do not exist");
                    if (!Db.Set<Server.Model.Attachment.Attachment>().Any(it => it.Id == attachment.Id))
                        throw new ArgumentException("Cant link. Attachment do not exist");
                    if (permissions == null)
                    {
                        permissions = Db.Set<Permission>().Where(it => it.IsActive).ToList();
                        isOwner = true;
                    }
                    foreach (var permission in permissions)
                        Db.Set<AttachmentPermission>().Add(new AttachmentPermission
                        {
                            PermissionId = permission.Id,
                            AttachmentId = attachment.Id,
                            UserId = entity.Id,
                            IsOwner = isOwner
                        });

                    Db.SaveChanges();
         //           tran.Commit();
                }
                catch (Exception ex)
                {
                    LogEventManager.Logger.Error(ex.Message,ex);
             //       tran.Rollback();
                    throw;
                }
         //   }
        }
        /// <summary>
        /// Removes attachment links to entity
        /// </summary>
        /// <param name="entity">entity (user)</param>
        /// <param name="attachment">attachment </param>
        /// <param name="permissions">Remove selected permissions. If null provided - removes all linked entities</param>
        public void RemoveAttachment(User entity, Server.Model.Attachment.Attachment attachment, ICollection<Permission> permissions = null)
        {
            //using (var tran = Db.Database.BeginTransaction())
            //{
                try
                {
                    if (!Db.Set<User>().Any(it => it.Id == entity.Id))
                        throw new ArgumentException("Can't Remove. User do not exist.");
                    if (!Db.Set<Server.Model.Attachment.Attachment>().Any(it => it.Id == attachment.Id))
                        throw new ArgumentException("Can't Remove. Attachment do not exist.");
                    if (permissions == null)
                    {
                        var attachPerms =
                            Db.Set<AttachmentPermission>()
                                .Where(it => it.UserId == entity.Id && it.AttachmentId == attachment.Id);
                        foreach (var attachmentPermission in attachPerms)
                            Db.Set<AttachmentPermission>().Remove(attachmentPermission);
                    }
                    else
                    {
                        foreach (var permission in permissions)
                        {
                            var attachPerm =
                                Db.Set<AttachmentPermission>()
                                    .Single(
                                        it =>
                                            it.UserId == entity.Id && it.AttachmentId == attachment.Id &&
                                            permission.Id == it.PermissionId);
                            Db.Set<AttachmentPermission>().Remove(attachPerm);
                        }
                    }

                    Db.SaveChanges();
            //        tran.Commit();
                }
                catch (Exception ex)
                {
                    LogEventManager.Logger.Error(ex.Message, ex);
           //         tran.Rollback();
                    throw;
                }
         //   }
        }



      
    }
}
