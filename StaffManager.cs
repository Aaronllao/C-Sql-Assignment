using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

namespace Assignment1
{
    class StaffManager
    {
        FormatManager fm = new FormatManager();

        public void ListStaff()
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
                    daUser.SelectCommand = new SqlCommand("select * from [User] where UserID like 'e%'", conn);
                    daSlot.SelectCommand = new SqlCommand("select * from Slot", conn);
                    daUser.Fill(ds, "userTable");
                    daSlot.Fill(ds, "slotTable");
                    userTable = ds.Tables["userTable"];
                    slotTable = ds.Tables["slotTable"];
                    conn.Open();
                    Console.WriteLine("--- List staffs ---");
                    Console.WriteLine("UserID        Name            Email");
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

        public void RoomAvaliability()
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
                    daUser.SelectCommand = new SqlCommand("select * from [User] where UserID like 'e%'", conn);
                    daSlot.SelectCommand = new SqlCommand("select * from Slot", conn);
                    daUser.Fill(ds, "userTable");
                    daSlot.Fill(ds, "slotTable");
                    userTable = ds.Tables["userTable"];
                    slotTable = ds.Tables["slotTable"];

                    conn.Open();
                    Console.WriteLine("---Room availability---");
                    Console.WriteLine("Enter date for room availability (dd-mm-yyyy):");
                    var selectedDate = Console.ReadLine();
                    fm.DateIsValid(selectedDate);
                    DateTime dt = DateTime.ParseExact(selectedDate, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                    Console.WriteLine("Enter date for room available on" + selectedDate + ":");
                    Console.WriteLine("\nRoom available on" + selectedDate + ":");
                    Console.WriteLine("Room Name");

                    var countA = 0;
                    var countB = 0;
                    var countC = 0;
                    var countD = 0;
                    foreach (DataRow row in slotTable.Rows)
                    {
                        if (row["Date"].Equals(dt))
                        {
                            if ((string)row["RoomID"] == "A")
                            {
                                countA++;
                            }
                            else if ((string)row["RoomID"] == "B")
                            {
                                countB++;
                            }
                            else if ((string)row["RoomID"] == "C")
                            {
                                countC++;
                            }
                            else if ((string)row["RoomID"] == "D")
                            {
                                countD++;
                            }
                        }

                    }
                    if (countA < 2)
                    {
                        Console.WriteLine("A");
                    }
                    if (countB < 2)
                    {
                        Console.WriteLine("B");
                    }
                    if (countC < 2)
                    {
                        Console.WriteLine("C");
                    }
                    if (countD < 2)
                    {
                        Console.WriteLine("D");
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

        public void CreateSlot()
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
                    StaffManager stfManager = new StaffManager();
                    daUser.SelectCommand = new SqlCommand("select * from [User] where UserID like 'e%'", conn);
                    daSlot.SelectCommand = new SqlCommand("select * from Slot", conn);
                    SqlCommandBuilder cb = new SqlCommandBuilder(daSlot);
                    daUser.Fill(ds, "userTable");
                    daSlot.Fill(ds, "slotTable");
                    userTable = ds.Tables["userTable"];
                    slotTable = ds.Tables["slotTable"];
                    conn.Open();
                    Console.WriteLine("--- Create slot ---");

                    Console.WriteLine("Enter room name:");
                    var roomID = (Console.ReadLine()).ToUpper();
                    fm.RoomIsValid(roomID);

                    //enter the date and convert to Date
                    Console.WriteLine("Enter date for slot(dd - mm - yyyy):");
                    var selectedDate = Console.ReadLine();
                    fm.DateIsValid(selectedDate);
                    DateTime dt = DateTime.ParseExact(selectedDate, "dd-MM-yyyy", CultureInfo.InvariantCulture);

                    //enter the time and get start time and end time
                    Console.WriteLine(" Enter time for slot(HH: mm)");
                    var timeInput = Console.ReadLine();
                    fm.TimeIsValid(timeInput);
                    DateTime time = DateTime.ParseExact(timeInput, "HH:mm", CultureInfo.InvariantCulture);
                    DateTime open = DateTime.ParseExact("09:00", "HH:mm", CultureInfo.InvariantCulture);
                    DateTime close = DateTime.ParseExact("14:00", "HH:mm", CultureInfo.InvariantCulture);
                    DateTime actualTime = DateTime.ParseExact(selectedDate + " " + timeInput, "dd-MM-yyyy HH:mm", CultureInfo.InvariantCulture);

                    Console.WriteLine(" Enter staff ID:");
                    var id = (Console.ReadLine()).ToLower();
                    fm.StaffIdIsValid(id);
                    var countrRoom = 0;
                    var occupied = false;
                    var countStaff = 0;
                    foreach (DataRow row in slotTable.Rows)
                    {
                        if (dt.Equals(row["Date"]))
                        {
                            if ((string)row["RoomID"] == roomID)
                            {
                                countrRoom++;
                            }

                            if ((string)row["RoomID"] == roomID && row["StaffID"] != null && row["StartTime"].Equals(actualTime))
                            {
                                occupied = true;
                            }

                            if ((string)row["StaffID"] == id)
                            {
                                countStaff++;
                            }
                        }
                    }
                    var overtime1 = DateTime.Compare(open, time);
                    var overtime2 = DateTime.Compare(close, time);

                    if (countrRoom >= 2 || countStaff >= 4 || occupied == true || overtime1 == 1 || overtime2 == -1)
                    {
                        Console.WriteLine("Unable to create slot.");
                    }
                    else
                    {
                        DataRow newRow = slotTable.NewRow();
                        newRow["RoomID"] = roomID;
                        newRow["StartTime"] = actualTime;
                        newRow["EndTime"] = actualTime.AddHours(1);
                        newRow["StaffID"] = id;
                        newRow["Date"] = dt;
                        slotTable.Rows.Add(newRow);
                        daSlot.Update(ds, "slotTable");
                        Console.WriteLine("Slot created successfully.");
                    }
                }
                catch (SqlException se)
                {
                    Console.WriteLine("SQL Exception: {0}", se.Message);
                }catch(RoomIdException re)
                {
                    Console.WriteLine(re.Message);
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

        public void RemoveSlot()
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
                    daUser.SelectCommand = new SqlCommand("select * from [User] where UserID like 'e%'", conn);
                    daSlot.SelectCommand = new SqlCommand("select * from Slot", conn);
                    daUser.Fill(ds, "userTable");
                    daSlot.Fill(ds, "slotTable");
                    userTable = ds.Tables["userTable"];
                    slotTable = ds.Tables["slotTable"];
                    conn.Open();
                    Console.WriteLine("--- Remove slot ---");

                    Console.WriteLine("Enter room name:");
                    var roomID = (Console.ReadLine()).ToUpper();
                    fm.RoomIsValid(roomID);

                    Console.WriteLine("Enter date for slot(dd - mm - yyyy):");
                    var selectedDate = Console.ReadLine();
                    fm.DateIsValid(selectedDate);
                    DateTime dt = DateTime.ParseExact(selectedDate, "dd-MM-yyyy", CultureInfo.InvariantCulture);

                    Console.WriteLine(" Enter time for slot(HH: mm)");
                    var timeInput = Console.ReadLine();
                    fm.TimeIsValid(timeInput);
                    var startTime = DateTime.ParseExact(selectedDate + " " + timeInput, "dd-MM-yyyy HH:mm", CultureInfo.InvariantCulture);
                    var removeAble = false;
                    foreach (DataRow row in slotTable.Rows)
                    {
                        if ((string)row["RoomID"] == roomID && dt.Equals(row["Date"])
                             && startTime.Equals(row["StartTime"]) && row["BookedInStudentID"] == DBNull.Value)
                        {
                            string delete = "delete from slot where  RoomID = @RoomID And Date = @Date"
                            + " And StartTime = @StartTime";
                            SqlCommand deleteComm = new SqlCommand(delete, conn);
                            deleteComm.Parameters.AddWithValue("@RoomID", roomID);
                            deleteComm.Parameters.AddWithValue("@Date", dt);
                            deleteComm.Parameters.AddWithValue("@StartTime", startTime);
                            deleteComm.ExecuteNonQuery();
                            removeAble = true;

                        }
                    }
                    if (removeAble)
                    {
                        Console.WriteLine("Slot removed successfully.");
                    }
                    else
                    {
                        Console.WriteLine("Unable to remove slot.");
                    }
                }
                catch (SqlException se)
                {
                    Console.WriteLine("SQL Exception: {0}", se.Message);
                }catch(RoomIdException re)
                {
                    Console.WriteLine(re.Message);
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
    }
}
