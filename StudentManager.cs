using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

namespace Assignment1
{
    class StudentManager
    {
        FormatManager fm = new FormatManager();

        public void ListStudent()
        {
            using (SqlConnection conn = new SqlConnection("server=wdt2019.australiasoutheast.cloudapp.azure.com;" +
                "uid=s3655108;database=s3655108;pwd=abc123;"))
            {
                try
                {
                    SqlDataAdapter daUser = new SqlDataAdapter();
                    SqlDataAdapter daSlot = new SqlDataAdapter();
                    DataTable userTable;
                    DataTable slotTable;
                    DataSet ds = new DataSet();
                    daUser.SelectCommand = new SqlCommand("select * from [User] where UserID like 's%'", conn);
                    daSlot.SelectCommand = new SqlCommand("select * from Slot", conn);
                    daUser.Fill(ds, "userTable");
                    daSlot.Fill(ds, "slotTable");
                    userTable = ds.Tables["userTable"];
                    slotTable = ds.Tables["slotTable"];
                    conn.Open();
                    Console.WriteLine("--- List students ---");
                    Console.WriteLine("UserID" + "  " + "Name" + "    " + "Email");
                    foreach (DataRow row in userTable.Rows)
                    {
                        Console.WriteLine("{0},{1},{2}", row["UserID"], "    " + row["Name"], "     " + row["Email"]);
                    }
                }
                catch (SqlException se)
                {
                    Console.WriteLine("SQL Exception: {0}", se.Message);
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public void StaffAvaliability()
        {
            using (SqlConnection conn = new SqlConnection("server=wdt2019.australiasoutheast.cloudapp.azure.com;" +
               "uid=s3655108;database=s3655108;pwd=abc123;"))
            {
                try
                {
                    SqlDataAdapter daUser = new SqlDataAdapter();
                    SqlDataAdapter daSlot = new SqlDataAdapter();
                    DataTable userTable;
                    DataTable slotTable;
                    DataSet ds = new DataSet();
                    daUser.SelectCommand = new SqlCommand("select * from [User] where UserID like 's%'", conn);
                    daSlot.SelectCommand = new SqlCommand("select * from Slot", conn);
                    daUser.Fill(ds, "userTable");
                    daSlot.Fill(ds, "slotTable");
                    userTable = ds.Tables["userTable"];
                    slotTable = ds.Tables["slotTable"];

                    conn.Open();
                    Console.WriteLine("--- Staff availability ---");
                    Console.WriteLine("Enter date for staff availability (dd - mm - yyyy):");
                    var selectedDate = Console.ReadLine();
                    fm.DateIsValid(selectedDate);
                    DateTime dt = DateTime.ParseExact(selectedDate, "dd-MM-yyyy", CultureInfo.InvariantCulture);

                    Console.WriteLine("Enter staff ID:");
                    var staffID = (Console.ReadLine()).ToLower();
                    fm.StaffIdIsValid(staffID);

                    Console.WriteLine("Staff" + staffID + "availability on " + selectedDate + ":");
                    Console.WriteLine("Room Name       Start Time      End Time");
                    foreach (DataRow row in slotTable.Rows)
                    {
                        if (row["BookedInStudentID"] == DBNull.Value && dt.Equals(row["Date"]) && (string)row["StaffID"] == staffID)
                        {
                            Console.WriteLine("{0},{1},{2}", row["RoomID"], "    " + row["StartTime"], "      " + row["EndTime"]);
                        }
                    }
                }
                catch (SqlException se)
                {
                    Console.WriteLine("SQL Exception: {0}", se.Message);
                }catch(UserIdException ue)
                {
                    Console.WriteLine(ue.Message);
                }catch(DateException de)
                {
                    Console.WriteLine(de.Message);
                }
                finally
                {
                    conn.Close();
                }
            }
        }


        public void MakeBooking()
        {
            using (SqlConnection conn = new SqlConnection("server=wdt2019.australiasoutheast.cloudapp.azure.com;" +
                "uid=s3655108;database=s3655108;pwd=abc123;"))
            {
                try
                {
                    SqlDataAdapter daUser = new SqlDataAdapter();
                    SqlDataAdapter daSlot = new SqlDataAdapter();
                    DataSet ds = new DataSet();
                    DataTable userTable;
                    DataTable slotTable;
                    daUser.SelectCommand = new SqlCommand("select * from [User] where UserID like 's%'", conn);
                    daSlot.SelectCommand = new SqlCommand("select * from Slot", conn);
                    daUser.Fill(ds, "roomTable");
                    daSlot.Fill(ds, "slotTable");
                    userTable = ds.Tables["userTable"];
                    slotTable = ds.Tables["slotTable"];


                    conn.Open();
                    Console.WriteLine("--- Make booking ---");
                    Console.WriteLine("Enter room name:");
                    var roomId = (Console.ReadLine()).ToUpper();
                    fm.RoomIsValid(roomId);

                    Console.WriteLine("Enter date for slot availability (dd - mm - yyyy):");
                    var selectedDate = Console.ReadLine();
                    fm.DateIsValid(selectedDate);
                    DateTime dt = DateTime.ParseExact(selectedDate, "dd-MM-yyyy", CultureInfo.InvariantCulture);

                    Console.WriteLine("Enter time for slot (HH:mm): ");
                    var timeInput = Console.ReadLine();
                    fm.TimeIsValid(timeInput);
                    DateTime actualTime = DateTime.ParseExact(selectedDate + " " + timeInput, "dd-MM-yyyy HH:mm", CultureInfo.InvariantCulture);

                    Console.WriteLine("Enter student ID:");
                    var studentID = (Console.ReadLine()).ToLower();
                    fm.StudentIdIsValid(studentID);
                    var bookAble1 = false;
                    var bookAble2 = false;
                    var bookAble3 = true;
                    foreach (DataRow row in slotTable.Rows)
                    {
                        if (row["BookedInStudentID"] != DBNull.Value && dt.Equals(row["Date"]))
                        {
                            bookAble1 = false;
                        }
                        else
                        {
                            bookAble1 = true;
                        }

                        if (dt.Equals(row["Date"]) && (string)row["RoomID"] == roomId
                            && actualTime.Equals(row["StartTime"]) && row["BookedInStudentID"] == DBNull.Value
                            && !string.IsNullOrEmpty((string)row["StaffID"]))
                        {
                            bookAble2 = true;
                        }

                        if (row["BookedInStudentID"] != DBNull.Value)
                        {
                            if ((string)row["BookedInStudentID"] == studentID)
                            {
                                bookAble3 = false;
                            }
                        }
                    }


                    if (bookAble1 && bookAble2 && bookAble3)
                    {
                        foreach (DataRow row in slotTable.Rows)
                        {
                            if ((string)row["RoomID"] == roomId && actualTime.Equals(row["StartTime"]) && dt.Equals(row["Date"]))
                            {
                                string update = "Update Slot Set BookedInStudentID = @BookedInStudentID " +
                                    "WHERE RoomID = @RoomID And  Date = @Date And StartTime = @StartTime";
                                SqlCommand updateComm = new SqlCommand(update, conn);
                                updateComm.Parameters.AddWithValue("@RoomID", roomId);
                                updateComm.Parameters.AddWithValue("@Date", dt);
                                updateComm.Parameters.AddWithValue("@StartTime", actualTime);
                                updateComm.Parameters.AddWithValue("@BookedInStudentID", studentID);
                                updateComm.ExecuteNonQuery();
                                daSlot.Update(ds, "slotTable");
                                break;
                            }
                        }

                        Console.WriteLine("Slot booked successfully.");
                    }
                    else
                    {
                        Console.WriteLine("Unable to book slot.");
                    }
                }
                catch (SqlException se)
                {
                    Console.WriteLine("SQL Exception: {0}", se.Message);
                }catch(UserIdException ue)
                {
                    Console.WriteLine(ue.Message);
                }catch(RoomIdException re)
                {
                    Console.WriteLine(re.Message);
                }catch(DataException de)
                {
                    Console.WriteLine(de.Message);
                }
                finally
                {
                    conn.Close();
                }
            }

        }

        public void CancelBooking()
        {
            using (SqlConnection conn = new SqlConnection("server=wdt2019.australiasoutheast.cloudapp.azure.com;" +
                "uid=s3655108;database=s3655108;pwd=abc123;"))
            {
                try
                {
                    SqlDataAdapter daUser = new SqlDataAdapter();
                    SqlDataAdapter daSlot = new SqlDataAdapter();
                    DataSet ds = new DataSet();
                    DataTable userTable;
                    DataTable slotTable;
                    daUser.SelectCommand = new SqlCommand("select * from [User] where UserID like 's%'", conn);
                    daSlot.SelectCommand = new SqlCommand("select * from Slot", conn);
                    daUser.Fill(ds, "roomTable");
                    daSlot.Fill(ds, "slotTable");
                    userTable = ds.Tables["userTable"];
                    slotTable = ds.Tables["slotTable"];
                    conn.Open();
                    Console.WriteLine("--- Cancel booking ---");
                    Console.WriteLine("Enter room name:");
                    var roomId = (Console.ReadLine()).ToUpper();
                    fm.RoomIsValid(roomId);

                    Console.WriteLine("Enter date for slot availability (dd - mm - yyyy):");
                    var selectedDate = Console.ReadLine();
                    DateTime dt = DateTime.ParseExact(selectedDate, "dd-MM-yyyy", CultureInfo.InvariantCulture);

                    Console.WriteLine("Enter time for slot (HH:mm): ");
                    var timeInput = Console.ReadLine();
                    DateTime actualTime = DateTime.ParseExact(selectedDate + " " + timeInput, "dd-MM-yyyy HH:mm", CultureInfo.InvariantCulture);

                    foreach (DataRow row in slotTable.Rows)
                    {
                        if ((string)row["RoomID"] == roomId && dt.Equals(row["Date"])
                            && row["StartTime"].Equals(actualTime) && row["BookedInStudentID"] != DBNull.Value)
                        {
                            string delete = "delete from slot where  RoomID = @RoomID And Date = @Date"
                            + " And StartTime = @StartTime";
                            SqlCommand deleteComm = new SqlCommand(delete, conn);
                            deleteComm.Parameters.AddWithValue("@RoomID", roomId);
                            deleteComm.Parameters.AddWithValue("@Date", dt);
                            deleteComm.Parameters.AddWithValue("@StartTime", actualTime);
                            deleteComm.ExecuteNonQuery();
                            Console.WriteLine("Slot removed successfully.");
                        }
                    }
                }
                catch (SqlException se)
                {
                    Console.WriteLine("SQL Exception: {0}", se.Message);
                }catch(RoomIdException re)
                {
                    Console.WriteLine(re.Message);
                }
                finally
                {
                    conn.Close();
                }


            }
        }

    }
}
