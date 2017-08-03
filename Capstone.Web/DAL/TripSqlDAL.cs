using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Capstone.Web.Models;
using System.Data.SqlClient;

namespace Capstone.Web.DAL
{
    public class TripSqlDAL: ITripDAL
    {
        readonly string connectionString;
        const string SQL_GetAllUserTrips = "SELECT * FROM trip JOIN city_tours_user ON city_tours_user.id = trip.city_tours_user_id WHERE city_tours_user.id = @userId;";
        const string SQL_SaveNewTrip = "INSERT INTO trip VALUES(@userId, @name, @description, @trip_date); SELECT SCOPE_IDENTITY();";
        const string SQL_SaveTripLandmark = "INSERT INTO trip_landmark VALUES(@tripId, @landmarkId, @visitOrder);";
        const string SQL_DeleteTrip = "DELETE FROM trip WHERE id = @tripId;";
        const string SQL_DeleteTrip_Landmark = "DELETE FROM trip_landmark WHERE trip_id = @tripId;";

        public TripSqlDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<Trip> GetAllUserTrips(int userId)
        {
            try
            {
                using(SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(SQL_GetAllUserTrips, conn);
                    cmd.Parameters.AddWithValue("@userId", userId);

                    List<Trip> userTrips = new List<Trip>();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while(reader.Read())
                    {
                        userTrips.Add(PopulateTripObject(reader));
                    }

                    return userTrips;
                }
            }
            catch(SqlException)
            {
                throw;
            }
        }

        public int SaveNewTrip(Trip trip, int userId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(SQL_SaveNewTrip, conn);
                    cmd.Parameters.AddWithValue("userId", userId);
                    cmd.Parameters.AddWithValue("name", trip.Name);
                    cmd.Parameters.AddWithValue("description", trip.Description);
                    cmd.Parameters.AddWithValue("trip_date", trip.TripDate);

                    int newTripId = Convert.ToInt32(cmd.ExecuteScalar());
                    return newTripId;
                }
            }
            catch (SqlException)
            {
                throw;
            }
        }

        public bool SaveTripLandmark(int tripId, int landmarkId, int visitOrder)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(SQL_SaveTripLandmark, conn);
                    cmd.Parameters.AddWithValue("tripId", tripId);
                    cmd.Parameters.AddWithValue("landmarkId", landmarkId);
                    cmd.Parameters.AddWithValue("visitOrder", visitOrder);

                    int numRowsAffected = cmd.ExecuteNonQuery();
                    return numRowsAffected > 0;
                }
            }
            catch(SqlException)
            {
                throw;
            }
        }

        public bool DeleteTrip(int tripId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(SQL_DeleteTrip, conn);
                    cmd.Parameters.AddWithValue("tripId", tripId);
                   
                    int numRowsAffected = cmd.ExecuteNonQuery();
                    return numRowsAffected > 0;
                }
            }
            catch (SqlException)
            {
                throw;
            }
        }

        public bool DeleteTripLandmark(int tripId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(SQL_DeleteTrip_Landmark, conn);
                    cmd.Parameters.AddWithValue("tripId", tripId);

                    int numRowsAffected = cmd.ExecuteNonQuery();
                    return numRowsAffected > 0;
                }
            }
            catch (SqlException)
            {
                throw;
            }
        }

        private Trip PopulateTripObject(SqlDataReader reader)
        {
            Trip trip = new Trip();
            trip.Id = Convert.ToInt32(reader["id"]);
            trip.Name = Convert.ToString(reader["name"]);
            trip.Description = Convert.ToString(reader["description"]);
            trip.TripDate = Convert.ToDateTime(reader["trip_date"]);

            return trip;
        }
    }
}