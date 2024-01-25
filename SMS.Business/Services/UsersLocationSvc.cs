using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SMS.Entities;
using SMS.Core;
using System.Linq.Expressions;

namespace SMS.Business.Service
{
    public class UsersInLocationSvc : EntityBaseSvc<UsersInLocation>, IEntitySvc<UsersInLocation, UsersInLocation, UsersInLocation>
    {
        public UsersInLocationSvc(ILogger logger)
            : base(logger, "User Location")
        {

        }

        public UsersInLocation GetServiceFilters()
        {
            UsersInLocation UsersInLocation = new UsersInLocation();
            return new UsersInLocation();
        }

        public IQueryable<UsersInLocation> Get()
        {
            return Db.UsersInLocations;
        }

        public UsersInLocation GetModel(Guid? userId)
        {
            return new UsersInLocation
            {

            };

        }

        public UsersInLocation GetModel(Guid id)
        {
            var UsersInLocation = this.Get(id);
            return UsersInLocation;
        }

        public UsersInLocation GetModel(Guid id, Guid? userId)
        {
            return id == Guid.Empty ? this.GetModel(id) : this.GetModel(userId);
        }

        public UsersInLocation Get(Guid id)
        {
            return Db.UsersInLocations.SingleOrDefault(e => e.TableRowGuid == id);
        }

        public JsTable<UsersInLocation> Get(PageDetail pageDetail)
        {
            var query = Db.UsersInLocations.AsQueryable();

            List<Expression<Func<UsersInLocation, bool>>> filters = new List<Expression<Func<UsersInLocation, bool>>>();

            if (!string.IsNullOrEmpty(pageDetail.Search)) filters.Add(m => m.UserGuid.ToString().Contains(pageDetail.Search));


            Func<UsersInLocation, UsersInLocation> result = e => e;

            return GridQueryHelper.JsTable(query, pageDetail, filters, result);
        }

        public Response Validate(UsersInLocation UsersInLocation)
        {
            Response response = new Response();

            if (UsersInLocation.TableRowGuid == Guid.Empty)
            {
                var query = Db.UsersInLocations.Where(r => r.UserGuid == UsersInLocation.UserGuid && r.LocationGuid == UsersInLocation.LocationGuid);
                if (query.ToList().Count > 0)
                {
                    response.Success = false;
                    response.Message = "A Users In Location by the user and location provided has already been created. Please provide a different Users In Location.";
                }
                else
                {
                    response.Success = true;
                }
            }
            else
            {
                var query = Db.UsersInLocations.Where(r => r.UserGuid == UsersInLocation.UserGuid && r.LocationGuid == UsersInLocation.LocationGuid && r.TableRowGuid != UsersInLocation.TableRowGuid);
                if (query.ToList().Count > 0)
                {
                    response.Success = false;
                    response.Message = "A Users In Location by the user and location provided has already been created. Please provide a different Users In Location.";
                }
                else
                {
                    response.Success = true;
                }
            }

            return response;
        }

        public Response Add(UsersInLocation UsersInLocation, Guid? userid)
        {
            Response response = new Response();

            response = Validate(UsersInLocation);
            if (!response.Success) return response;

            UsersInLocation.TableRowGuid = Guid.NewGuid();
            UsersInLocation.UserId = userid;

            return base.Add(UsersInLocation);
        }

        public Response Update(UsersInLocation UsersInLocation, Guid? userid)
        {
            Response response = new Response();

            response = Validate(UsersInLocation);
            if (!response.Success) return response;

            UsersInLocation.UserId = userid;

            return base.Update(UsersInLocation);
        }

        public Response Delete(UsersInLocation UsersInLocation, Guid? userid)
        {
            UsersInLocation.UserId = userid;
            return base.Delete(UsersInLocation);
        }

        public Response Delete(Guid id, Guid? userid)
        {
            return this.Delete(new UsersInLocation() { TableRowGuid = id }, userid);
        }

        public Guid[] GetUserLocations(Guid id)
        {
           
            var locationGuids = from r in Db.UsersInLocations.Where(x => x.UserGuid == id)
                            select (Guid)r.LocationGuid;

            return locationGuids.ToArray();

        }

        public Response SaveUserLocations(UserLocation userLocations, Guid? userid, int eventSource, string computerName)
        {
            List<UsersInLocation> listOfLocations = new List<UsersInLocation>();
            var currentUserLocations = (from c in Db.UsersInLocations
                                       where c.UserGuid == userLocations.UserId
                                       select (Guid)c.LocationGuid).ToArray();

            try
            {
                //Locations to delete
                for (int i = 0; i < currentUserLocations.Count(); i++)
                {
                    if (!userLocations.SelectedUserLocations.Contains(currentUserLocations[i]))
                    {

                        Guid locationguid = currentUserLocations[i];
                        UsersInLocation userLocation = (from c in Db.UsersInLocations
                                                       where c.UserGuid == userLocations.UserId
                                                       && c.LocationGuid == locationguid
                                                       select c).SingleOrDefault();

                        listOfLocations.Add(userLocation);
                        
                    }
                    
                }
                Delete(listOfLocations);
                listOfLocations.Clear();

                // Locations to save
                for (int i = 0; i < userLocations.SelectedUserLocations.Count(); i++)
                {
                    if (!currentUserLocations.Contains(userLocations.SelectedUserLocations[i]))
                    {
                        UsersInLocation userLocation = new UsersInLocation
                        {
                            UserGuid = userLocations.UserId,
                            LocationGuid = userLocations.SelectedUserLocations[i],
                            UserId =  userid,
                            EventSource = eventSource,
                            ComputerName = computerName,
                            TableRowGuid = Guid.NewGuid()
                        };
                        listOfLocations.Add(userLocation);                       
                    }
                }
                if(listOfLocations.Count() > 0)
                    base.Add(listOfLocations);

                return new SuccessResponse("Location Data Saved Successfully");
            }
            catch (Exception ex)
            {
                return new FailedResponse(ex.Message);
            }
                
        }
    }

}
