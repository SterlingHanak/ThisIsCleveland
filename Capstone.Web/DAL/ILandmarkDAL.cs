using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capstone.Web.Models;

namespace Capstone.Web.DAL
{
    public interface ILandmarkDAL
    {
        List<string> GetAllCategories();
        Landmark GetLandmark(int landmarkId);
        List<Landmark> GetAllLandmarks();
        List<Landmark> GetAllLandmarksInCategory(string category);
        List<Landmark> GetAllLandmarksInTrip(int tripId);
        List<string> GetLandmarkCategories(int landmarkId);
        Dictionary<string, Hours> GetLandmarkSchedule(int landmarkId);
    }
}
