using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using RazorHotelDB21.Model;

namespace RazorHotelDB21.Services {
    public class HotelService : AsyncService<Hotel> {
        
        public HotelService(string relationalName, string[] relationalKeys, string[] relationalAttributes) : base(relationalName, relationalKeys, relationalAttributes) { }
        public HotelService() : this("Hotel", new string[] { "Hotel_No" }, new string[] { "Name", "Address" }) {}

        public override async Task<bool> CreateItem(Hotel hotel) {
            try {
                using (SqlConnection connection = new SqlConnection(connectionString)) {
                    using (SqlCommand command = new SqlCommand(SQLInsert, connection)) {

                        command.Parameters.AddWithValue($"@{_relationalKeys[0]}", hotel.Hotel_No);
                        command.Parameters.AddWithValue($"@{_relationalAttributes[0]}", hotel.Name);
                        command.Parameters.AddWithValue($"@{_relationalAttributes[1]}", hotel.Address);

                        await command.Connection.OpenAsync();

                        int i = await command.ExecuteNonQueryAsync();
                        if (i == 0) {
                            return false;
                        } else {
                            return true;
                        }
                    }
                }
            } catch (Exception) {

            }
            return false;
        }

        public override async Task<Hotel> DeleteItem(int[] ids) {
            try {
                using (SqlConnection connection = new SqlConnection(connectionString)) {
                    using (SqlCommand command = new SqlCommand(SQLDelete, connection)) {
                        command.Parameters.AddWithValue($"@{_relationalKeys[0]}", ids[0]);

                        await command.Connection.OpenAsync();

                        Hotel re = await GetItem(ids);

                        _ = command.ExecuteNonQueryAsync();
                        return re;
                    }
                }
            } catch (Exception) {
                
            }
            return null;
        }

        public override async Task<List<Hotel>> GetAllItems() {
            List<Hotel> hotels = new List<Hotel>();

            try {
                using (SqlConnection connection = new SqlConnection(connectionString)) {
                    using (SqlCommand command = new SqlCommand(SQLGetAll, connection)) {

                        await command.Connection.OpenAsync();

                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read()) {
                            int hotel_No = reader.GetInt32(0);
                            string name = reader.GetString(1);
                            string address = reader.GetString(2);
                            Hotel hotel = new Hotel(hotel_No, name, address);
                            hotels.Add(hotel);
                        }
                    }
                }
            } catch (Exception) {
                
            }
            return hotels;
        }

        public override async Task<List<Hotel>> GetItemsWithKey(int keyNr, object key) {
            List<Hotel> hotels = new List<Hotel>();

            try {
                using (SqlConnection connection = new SqlConnection(connectionString)) {
                    using (SqlCommand command = new SqlCommand(SQLGetFromKey(keyNr, key.ToString()), connection)) {

                        await command.Connection.OpenAsync();

                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read()) {
                            int hotel_No = reader.GetInt32(0);
                            string name = reader.GetString(1);
                            string address = reader.GetString(2);
                            Hotel hotel = new Hotel(hotel_No, name, address);
                            hotels.Add(hotel);
                        }
                    }
                }
            } catch (Exception) {

            }
            return hotels;
        }

        public override async Task<List<Hotel>> GetItemsWithAttribute(int attributeNr, object attribute) {
            List<Hotel> hotels = new List<Hotel>();

            try {
                using (SqlConnection connection = new SqlConnection(connectionString)) {
                    using (SqlCommand command = new SqlCommand(SQLGetFromAtttribute(attributeNr, attribute.ToString()), connection)) {

                        await command.Connection.OpenAsync();

                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read()) {
                            int hotel_No = reader.GetInt32(0);
                            string name = reader.GetString(1);
                            string address = reader.GetString(2);
                            Hotel hotel = new Hotel(hotel_No, name, address);
                            hotels.Add(hotel);
                        }
                    }
                }
            } catch (Exception) {

            }
            return hotels;
        }
        public override async Task<List<Hotel>> GetItemsWithKeyLike(int keyNr, string key) {
            List<Hotel> hotels = new List<Hotel>();

            try {
                using (SqlConnection connection = new SqlConnection(connectionString)) {
                    using (SqlCommand command = new SqlCommand(SQLGetLikeKey(keyNr, key.ToString()), connection)) {

                        await command.Connection.OpenAsync();

                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read()) {
                            int hotel_No = reader.GetInt32(0);
                            string name = reader.GetString(1);
                            string address = reader.GetString(2);
                            Hotel hotel = new Hotel(hotel_No, name, address);
                            hotels.Add(hotel);
                        }
                    }
                }
            } catch (Exception) {

            }
            return hotels;
        }

        public override async Task<List<Hotel>> GetItemsWithAttributeLike(int attributeNr, string attribute) {
            List<Hotel> hotels = new List<Hotel>();

            try {
                using (SqlConnection connection = new SqlConnection(connectionString)) {
                    using (SqlCommand command = new SqlCommand(SQLGetLikeAtttribute(attributeNr, attribute.ToString()), connection)) {

                        await command.Connection.OpenAsync();

                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read()) {
                            int hotel_No = reader.GetInt32(0);
                            string name = reader.GetString(1);
                            string address = reader.GetString(2);
                            Hotel hotel = new Hotel(hotel_No, name, address);
                            hotels.Add(hotel);
                        }
                    }
                }
            } catch (Exception) {

            }
            return hotels;
        }
        public override async Task<Hotel> GetItem(int[] ids) {
            List<Hotel> hotels = new List<Hotel>();

            try {
                using (SqlConnection connection = new SqlConnection(connectionString)) {
                    using (SqlCommand command = new SqlCommand(SQLGet, connection)) {
                        command.Parameters.AddWithValue($"@{_relationalKeys[0]}", ids[0]);

                        await command.Connection.OpenAsync();

                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read()) {
                            int hotel_No = reader.GetInt32(0);
                            string name = reader.GetString(1);
                            string address = reader.GetString(2);
                            Hotel hotel = new Hotel(hotel_No, name, address);
                            hotels.Add(hotel);
                        }
                    }
                }
            } catch (Exception) {

            }
            if(hotels.Count > 0) {
                return hotels[0];
            }
            return null;
        }

        public override async Task<bool> UpdateItem(Hotel hotel, int[] ids) {
            try {
                using (SqlConnection connection = new SqlConnection(connectionString)) {
                    using (SqlCommand command = new SqlCommand(SQLUpdate, connection)) {

                        command.Parameters.AddWithValue($"@{_relationalKeys[0]}", hotel.Hotel_No);
                        command.Parameters.AddWithValue($"@{_relationalAttributes[0]}", hotel.Name);
                        command.Parameters.AddWithValue($"@{_relationalAttributes[1]}", hotel.Address);
                        command.Parameters.AddWithValue($"@To_Update_0", ids[0]);

                        await command.Connection.OpenAsync();

                        int i = await command.ExecuteNonQueryAsync();
                        if (i == 0) {
                            return false;
                        } else {
                            return true;
                        }
                    }
                }
            } catch (Exception) {

            }
            return false;
        }
    }
}