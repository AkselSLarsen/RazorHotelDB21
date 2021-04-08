using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using RazorHotelDB21.Model;

namespace RazorHotelDB21.Services {
    public class RoomService : AsyncService<Room> {
        public RoomService(string relationalName, string[] relationalKeys, string[] relationalAttributes) : base(relationalName, relationalKeys, relationalAttributes) { }
        public RoomService() : this("Room", new string[] { "Room_No", "Hotel_No" }, new string[] { "Types", "Price" }) {}

        public override async Task<bool> CreateItem(Room room) {
            try {
                using (SqlConnection connection = new SqlConnection(connectionString)) {
                    using (SqlCommand command = new SqlCommand(SQLInsert, connection)) {

                        command.Parameters.AddWithValue($"@{_relationalKeys[0]}", room.Room_No);
                        command.Parameters.AddWithValue($"@{_relationalKeys[1]}", room.Hotel_No);
                        command.Parameters.AddWithValue($"@{_relationalAttributes[0]}", room.Types.ToString());
                        command.Parameters.AddWithValue($"@{_relationalAttributes[1]}", room.Price);

                        await command.Connection.OpenAsync();

                        int i = command.ExecuteNonQuery();
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

        public override async Task<Room> DeleteItem(int[] ids) {
            try {
                using (SqlConnection connection = new SqlConnection(connectionString)) {
                    using (SqlCommand command = new SqlCommand(SQLDelete, connection)) {
                        command.Parameters.AddWithValue($"@{_relationalKeys[0]}", ids[0]);
                        command.Parameters.AddWithValue($"@{_relationalKeys[1]}", ids[1]);

                        await command.Connection.OpenAsync();

                        Room re = await GetItem(ids);

                        _ = command.ExecuteNonQuery();
                        return re;
                    }
                }
            } catch (Exception) {
                
            }
            return null;
        }

        public override async Task<List<Room>> GetAllItems() {
            List<Room> rooms = new List<Room>();

            try {
                using (SqlConnection connection = new SqlConnection(connectionString)) {
                    using (SqlCommand command = new SqlCommand(SQLGetAll, connection)) {

                        await command.Connection.OpenAsync();

                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read()) {
                            int room_No = reader.GetInt32(0);
                            int hotel_No = reader.GetInt32(1);
                            RoomTypes types = GetType(reader.GetString(2));
                            double price = reader.GetDouble(3);
                            Room room = new Room(room_No, hotel_No, types, price);
                            rooms.Add(room);
                        }
                    }
                }
            } catch (Exception) {

            }
            return rooms;
        }

        public override async Task<List<Room>> GetItemsWithKey(int keyNr, object key) {
            List<Room> rooms = new List<Room>();

            try {
                using (SqlConnection connection = new SqlConnection(connectionString)) {
                    using (SqlCommand command = new SqlCommand(SQLGetFromKey(keyNr, key.ToString()), connection)) {

                        await command.Connection.OpenAsync();

                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read()) {
                            int room_No = reader.GetInt32(0);
                            int hotel_No = reader.GetInt32(1);
                            RoomTypes types = GetType(reader.GetString(2));
                            double price = reader.GetDouble(3);
                            Room room = new Room(room_No, hotel_No, types, price);
                            rooms.Add(room);
                        }
                    }
                }
            } catch (Exception) {

            }
            return rooms;
        }

        public override async Task<List<Room>> GetItemsWithAttribute(int attributeNr, object attribute) {
            List<Room> rooms = new List<Room>();

            try {
                using (SqlConnection connection = new SqlConnection(connectionString)) {
                    using (SqlCommand command = new SqlCommand(SQLGetFromAtttribute(attributeNr, attribute.ToString()), connection)) {

                        await command.Connection.OpenAsync();

                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read()) {
                            int room_No = reader.GetInt32(0);
                            int hotel_No = reader.GetInt32(1);
                            RoomTypes types = GetType(reader.GetString(2));
                            double price = reader.GetDouble(3);
                            Room room = new Room(room_No, hotel_No, types, price);
                            rooms.Add(room);
                        }
                    }
                }
            } catch (Exception) {

            }
            return rooms;
        }

        public override async Task<List<Room>> GetItemsWithKeyLike(int keyNr, string key) {
            List<Room> rooms = new List<Room>();

            try {
                using (SqlConnection connection = new SqlConnection(connectionString)) {
                    using (SqlCommand command = new SqlCommand(SQLGetLikeKey(keyNr, key.ToString()), connection)) {

                        await command.Connection.OpenAsync();

                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read()) {
                            int room_No = reader.GetInt32(0);
                            int hotel_No = reader.GetInt32(1);
                            RoomTypes types = GetType(reader.GetString(2));
                            double price = reader.GetDouble(3);
                            Room room = new Room(room_No, hotel_No, types, price);
                            rooms.Add(room);
                        }
                    }
                }
            } catch (Exception) {

                throw;
            }
            return rooms;
        }

        public override async Task<List<Room>> GetItemsWithAttributeLike(int attributeNr, string attribute) {
            List<Room> rooms = new List<Room>();

            try {
                using (SqlConnection connection = new SqlConnection(connectionString)) {
                    using (SqlCommand command = new SqlCommand(SQLGetLikeAtttribute(attributeNr, attribute.ToString()), connection)) {

                        await command.Connection.OpenAsync();

                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read()) {
                            int room_No = reader.GetInt32(0);
                            int hotel_No = reader.GetInt32(1);
                            RoomTypes types = GetType(reader.GetString(2));
                            double price = reader.GetDouble(3);
                            Room room = new Room(room_No, hotel_No, types, price);
                            rooms.Add(room);
                        }
                    }
                }
            } catch (Exception) {

            }
            return rooms;
        }

        public override async Task<Room> GetItem(int[] ids) {
            List<Room> rooms = new List<Room>();

            try {
                using (SqlConnection connection = new SqlConnection(connectionString)) {
                    using (SqlCommand command = new SqlCommand(SQLGet, connection)) {
                        command.Parameters.AddWithValue($"@{_relationalKeys[0]}", ids[0]);
                        command.Parameters.AddWithValue($"@{_relationalKeys[1]}", ids[1]);

                        await command.Connection.OpenAsync();

                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read()) {
                            RoomTypes types = GetType(reader.GetString(2));
                            double price = reader.GetDouble(3);
                            Room room = new Room(ids[0], ids[1], types, price);
                            rooms.Add(room);
                        }
                    }
                }
            } catch (Exception) {

            }
            if (rooms.Count > 0) {
                return rooms[0];
            }
            return null;
        }

        public override async Task<bool> UpdateItem(Room room, int[] ids) {
            try {
                using (SqlConnection connection = new SqlConnection(connectionString)) {
                    using (SqlCommand command = new SqlCommand(SQLUpdate, connection)) {
                        command.Parameters.AddWithValue($"@{_relationalKeys[0]}", room.Room_No);
                        command.Parameters.AddWithValue($"@{_relationalKeys[1]}", room.Hotel_No);
                        command.Parameters.AddWithValue($"@{_relationalAttributes[0]}", room.Types.ToString());
                        command.Parameters.AddWithValue($"@{_relationalAttributes[1]}", room.Price);
                        command.Parameters.AddWithValue($"@To_Update_0", ids[0]);
                        command.Parameters.AddWithValue($"@To_Update_1", ids[1]);

                        await command.Connection.OpenAsync();

                        int i = command.ExecuteNonQuery();
                        if (i == 0) {
                            return false;
                        } else {
                            return true;
                        }
                    }
                }
            } catch (Exception) {
                return false;
            }
        }

        private RoomTypes GetType(string s) {
            switch (s) {
                case "S":
                    return RoomTypes.S;
                case "D":
                    return RoomTypes.D;
                case "F":
                    return RoomTypes.F;
                default:
                    throw new ArgumentException("Room type must be either \"S\", \"D\" or \"F\"");
            }
        }
    }
}