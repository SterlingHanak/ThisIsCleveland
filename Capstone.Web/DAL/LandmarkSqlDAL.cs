using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Capstone.Web.Models;
using System.Data.SqlClient;

namespace Capstone.Web.DAL
{
    public class LandmarkSqlDAL : ILandmarkDAL
    {
        readonly string connectionString;

        // Query to retrieve all possible landmark categories
        const string SQL_GetAllCategories = "SELECT * FROM category;";

        // Queries for retrieving Landmark objects
        const string SQL_GetLandmark = "SELECT * FROM landmark WHERE id = @landmarkId;";
        const string SQL_GetAllLandmarks = "SELECT * FROM landmark ORDER BY name;";
        const string SQL_GetAllLandmarksInCategory = "SELECT * FROM landmark JOIN landmark_category ON landmark_category.landmark_id = landmark.id JOIN category ON category.id = landmark_category.category_id WHERE category.name = @category ORDER BY landmark.name;";
        const string SQL_GetAllLandmarksInTrip = "SELECT * FROM landmark JOIN trip_landmark ON trip_landmark.landmark_id = landmark.id WHERE trip_landmark.trip_id = @tripId;";
        const string SQL_GetAllLandmarksFromKeywords = "SELECT * FROM landmark LEFT JOIN park ON park.landmark_id = landmark.id LEFT JOIN park_activity ON park_activity.park_id = park.id LEFT JOIN restaurant ON restaurant.landmark_id = landmark.id LEFT JOIN landmark_category ON landmark_category.landmark_id = landmark.id LEFT JOIN category ON category.id = landmark_category.category_id WHERE LOWER(landmark.name) LIKE @search OR LOWER(landmark.address) LIKE @search OR LOWER(landmark.description) LIKE @search OR LOWER(park_activity.activity) LIKE @search OR LOWER(restaurant.cuisine_type) LIKE @search OR LOWER(category.name) LIKE @search;";

        // Queries for setting collection properties of Landmark
        const string SQL_GetLandmarkCategories = "SELECT name FROM category INNER JOIN landmark_category ON category_id =  category.id WHERE landmark_id = @landmarkId;";
        const string SQL_GetLandmarkSchedule = "SELECT day.name, daily_hours.time_open, daily_hours.time_closed FROM daily_hours JOIN day ON day.id = daily_hours.day_id JOIN landmark ON landmark.id = daily_hours.landmark_id WHERE landmark_id = @landmarkId;";

        public LandmarkSqlDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<string> GetAllCategories()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(SQL_GetAllCategories, conn);
                    List<string> listOfCategories = new List<string>();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        listOfCategories.Add(Convert.ToString(reader["name"]));
                    }
                    return listOfCategories;
                }
            }
            catch (SqlException)
            {
                throw;
            }
        }

        public Landmark GetLandmark(int landmarkId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(SQL_GetLandmark, conn);
                    cmd.Parameters.AddWithValue("@landmarkId", landmarkId);
                    SqlDataReader reader = cmd.ExecuteReader();
                    Landmark landmark = new Landmark();
                    while (reader.Read())
                    {
                        landmark = PopulateLandmarkObject(reader);
                    }
                    return landmark;
                }
            }
            catch (SqlException)
            {
                throw;
            }
        }

        public List<Landmark> GetAllLandmarks()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(SQL_GetAllLandmarks, conn);
                    List<Landmark> listOfLandmarks = new List<Landmark>();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        listOfLandmarks.Add(PopulateLandmarkObject(reader));
                    }
                    return listOfLandmarks;
                }
            }
            catch (SqlException)
            {
                throw;
            }
        }

        public List<Landmark> GetAllLandmarksInCategory(string category)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(SQL_GetAllLandmarksInCategory, conn);
                    cmd.Parameters.AddWithValue("@category", category);
                    SqlDataReader reader = cmd.ExecuteReader();
                    List<Landmark> landmarksInCategory = new List<Landmark>();
                    while (reader.Read())
                    {
                        landmarksInCategory.Add(PopulateLandmarkObject(reader));
                    }
                    return landmarksInCategory;
                }
            }
            catch (SqlException)
            {
                throw;
            }
        }

        public List<Landmark> GetAllLandmarksInTrip(int tripId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(SQL_GetAllLandmarksInTrip, conn);
                    cmd.Parameters.AddWithValue("@tripId", tripId);
                    SqlDataReader reader = cmd.ExecuteReader();
                    List<Landmark> landmarksInTrip = new List<Landmark>();
                    while (reader.Read())
                    {
                        landmarksInTrip.Add(PopulateLandmarkObject(reader));
                    }
                    return landmarksInTrip;
                }
            }
            catch(SqlException)
            {
                throw;
            }
        }

        public List<Landmark> GetAllLandmarksFromKeywords(string keywords)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(SQL_GetAllLandmarksFromKeywords, conn);
                    cmd.Parameters.AddWithValue("@search", "%" + keywords.ToLower() + "%");
                    var x = cmd.CommandText;
                    SqlDataReader reader = cmd.ExecuteReader();
                    List<Landmark> landmarksFromKeywords = new List<Landmark>();
                    while (reader.Read())
                    {
                        landmarksFromKeywords.Add(PopulateLandmarkObject(reader));
                    }
                    return landmarksFromKeywords;
                }
            }
            catch (SqlException)
            {
                throw;
            }
        }

        public List<string> GetLandmarkCategories(int landmarkId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(SQL_GetLandmarkCategories, conn);
                    cmd.Parameters.AddWithValue("@landmarkId", landmarkId);
                    SqlDataReader reader = cmd.ExecuteReader();
                    List<string> listOfCategories = new List<string>();
                    while (reader.Read())
                    {
                        listOfCategories.Add(Convert.ToString(reader["name"]));
                    }
                    return listOfCategories;
                }
            }
            catch (SqlException)
            {
                throw;
            }
        }

        public Dictionary<string, Hours> GetLandmarkSchedule(int landmarkId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(SQL_GetLandmarkSchedule, conn);
                    cmd.Parameters.AddWithValue("@landmarkId", landmarkId);
                    SqlDataReader reader = cmd.ExecuteReader();
                    Dictionary<string, Hours> schedule = new Dictionary<string, Hours>();
                    while (reader.Read())
                    {
                        Hours hours = new Hours();
                        hours.TimeOpen = Convert.ToDateTime(reader["time_open"]).ToShortTimeString();
                        hours.TimeClosed = Convert.ToDateTime(reader["time_closed"]).ToShortTimeString();
                        schedule[Convert.ToString(reader["name"])] = hours;
                    }
                    return schedule;
                }
            }
            catch (SqlException)
            {
                throw;
            }
        }

        private Landmark PopulateLandmarkObject(SqlDataReader reader)
        {
            Landmark landmark = new Landmark();
            landmark.Id = Convert.ToInt32(reader["id"]);
            landmark.Name = Convert.ToString(reader["name"]);
            landmark.Address = Convert.ToString(reader["address"]);
            landmark.PhoneNumber = Convert.ToString(reader["phone_number"]);
            landmark.Description = Convert.ToString(reader["description"]);
            if (reader["year_founded"] is DBNull)
            {
                landmark.YearFounded = null;
            }
            else
            {
                landmark.YearFounded = Convert.ToInt32(reader["year_founded"]);
            }
            landmark.AvgRating = Convert.ToInt32(reader["average_rating"]);
            landmark.RelativeCost = Convert.ToInt32(reader["relative_cost"]);
            if (reader["annual_num_visitors"] is DBNull)
            {
                landmark.AnnualNumVisitors = null;
            }
            else
            {
                landmark.AnnualNumVisitors = Convert.ToInt32(reader["annual_num_visitors"]);
            }
            landmark.WebsiteUrl = Convert.ToString(reader["website_url"]);
            landmark.Latitude = Convert.ToDouble(reader["latitude"]);
            landmark.Longitude = Convert.ToDouble(reader["longitude"]);
            return landmark;
        }

    }
}