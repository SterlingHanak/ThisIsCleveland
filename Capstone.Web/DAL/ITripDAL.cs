using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capstone.Web.Models;

namespace Capstone.Web.DAL
{
    public interface ITripDAL
    {
        List<Trip> GetAllUserTrips(int userId);
        int SaveNewTrip(Trip trip, int userId);
        bool SaveTripLandmark(int tripId, int landmarkId, int visitOrder);
        bool DeleteTripLandmark(int landmarkId);
    }
}
