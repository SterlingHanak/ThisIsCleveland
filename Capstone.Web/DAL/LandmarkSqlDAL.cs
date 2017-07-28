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
        private string connectionString;
        readonly string SQL_GetAllCategories = "SELECT * FROM category;";
        readonly string SQL_GetAllLandmarks = "SELECT * FROM landmark;";
        readonly string SQL_GetLandmark = "SELECT * FROM landmark WHERE id = @landmarkId;";
        readonly string SQL_GetLandmarkHighlights = "SELECT highlight FROM landmark_highlight WHERE landmark_id = @landmarkId;";
        readonly string SQL_GetLandmarkCategories = "SELECT name FROM category INNER JOIN landmark_category " +
                                                    "ON category_id =  category.id WHERE landmark_id = @landmarkId;";
        readonly string SQL_GetSchedule = "SELECT day.name, daily_hours.time_open, daily_hours.time_closed FROM daily_hours JOIN" +
                                           " day ON day.id = daily_hours.day_id JOIN landmark ON landmark.id = daily_hours.landmark_id " +
                                            "WHERE landmark_id = @landmarkId;";
        readonly string SQL_GetAllLandmarksInCategory = "SELECT * FROM landmark JOIN landmark_category ON landmark_category.landmark_id = landmark.id " +
                                            "JOIN category ON category.id = landmark_category.category_id WHERE category.name = @category;";

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

        public List<string> GetLandmarkCategories(int id)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(SQL_GetLandmarkCategories, conn);
                    cmd.Parameters.AddWithValue("@landmarkId", id);
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

        public List<string> GetLandmarkHighlights(int id)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(SQL_GetLandmarkHighlights, conn);
                    cmd.Parameters.AddWithValue("@landmarkId", id);
                    List<string> listOfHighlights = new List<string>();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        listOfHighlights.Add(Convert.ToString(reader["highlight"]));
                    }
                    return listOfHighlights;
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

        public Landmark GetLandmark(int id)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(SQL_GetLandmark, conn);
                    cmd.Parameters.AddWithValue("@landmarkId", id);
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

        public Dictionary<string, Hours> GetSchedule(int id)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(SQL_GetSchedule, conn);
                    cmd.Parameters.AddWithValue("@landmarkId", id);
                    SqlDataReader reader = cmd.ExecuteReader();
                    Dictionary<string, Hours> schedule = new Dictionary<string, Hours>();
                    while (reader.Read())
                    {
                        Hours hours = new Hours();
                        hours.TimeOpen = Convert.ToDateTime(reader["time_open"]);
                        hours.TimeClosed = Convert.ToDateTime(reader["time_closed"]);
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
            //landmark.AnnualNumVisitors = Convert.ToInt32(reader["annual_num_visitors"]);
            landmark.WebsiteUrl = Convert.ToString(reader["website_url"]);
            landmark.Latitude = Convert.ToDouble(reader["latitude"]);
            landmark.Longitude = Convert.ToDouble(reader["longitude"]);
            return landmark;
        }


    }
}