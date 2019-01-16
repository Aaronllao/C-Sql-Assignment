using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

namespace Assignment1
{
    class MainMenuManager
    {
        FormatManager fm = new FormatManager();
        public void ListRoom()
        {
            using (SqlConnection conn = new SqlConnection("server=wdt2019.australiasoutheast.cloudapp.azure.com;" +
                "uid=s3655108;database=s3655108;pwd=abc123;"))
            {
                try
                {
                    SqlDataAdapter daRoom = new SqlDataAdapter();
                    SqlDataAdapter daSlot = new SqlDataAdapter();
                    DataTable roomTable;
                    DataTable slotTable;
                    DataSet ds = new DataSet();
                    daRoom.SelectCommand = new SqlCommand("select * from Room", conn);
                    daSlot.SelectCommand = new SqlCommand("select * from Slot", conn);
                    daRoom.Fill(ds, "roomTable");
                    daSlot.Fill(ds, "slotTable");
                    roomTable = ds.Tables["roomTable"];
                    slotTable = ds.Tables["slotTable"];

                    Console.WriteLine("--- List rooms ---");
                    foreach (DataRow row in roomTable.Rows)
                    {
                        Console.WriteLine("{0}", row["RoomID"]);
                    }
                }catch(SqlException se)
                {
                    Console.WriteLine("SQL Exception: {0}", se.Message);
                }

            }
        }


        public void ListSlot()
        {
            using (SqlConnection conn = new SqlConnection("server=wdt2019.australiasoutheast.cloudapp.azure.com;" +
                "uid=s3655108;database=s3655108;pwd=abc123;"))
            {
                try
                {
                    SqlDataAdapter daRoom = new SqlDataAdapter();
                    SqlDataAdapter daSlot = new SqlDataAdapter();
                    DataTable roomTable;
                    DataTable slotTable;
                    DataSet ds = new DataSet();
                    daRoom.SelectCommand = new SqlCommand("select * from Room", conn);
                    daSlot.SelectCommand = new SqlCommand("select * from Slot", conn);
                    daRoom.Fill(ds, "roomTable");
                    daSlot.Fill(ds, "slotTable");
                    roomTable = ds.Tables["roomTable"];
                    slotTable = ds.Tables["slotTable"];

                    Console.WriteLine("--- List slots ---");
                    Console.WriteLine("Enter date for slots (dd-mm-yyyy):");
                    var input = Console.ReadLine();
                    fm.DateIsValid(input);
                    DateTime time = DateTime.ParseExact(input, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                    Console.WriteLine("RoomID           StartTime           EndTime             staffID         BookedInStudentID");
                    foreach (DataRow row1 in slotTable.Rows)
                    {
                        if (time.Equals(row1["Date"]))
                        {

                            Console.WriteLine("{0}{1}{2}{3}{4}", row1["RoomID"], "      " + row1["StartTime"], "      " + row1["EndTime"],
                                "        " + row1["staffID"], "       " + row1["BookedInStudentID"]);
                        }
                    }
                }
                catch(SqlException se)
                {
                    Console.WriteLine("SQL Exception: {0}", se.Message);
                }catch(DateException de)
                {
                    Console.WriteLine(de.Message);
                }
                
            }
        }
    }
}
