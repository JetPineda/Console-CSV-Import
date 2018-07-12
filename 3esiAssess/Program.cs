using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;


namespace _3esiAssess
{
    class Program
    {

        static void Main(string[] args)
        {
            //Boolean run = true;


            Console.WriteLine("Please Input File Path");
            string path = Console.ReadLine();

            if (!File.Exists(path))
            {
                Console.WriteLine("Please Provide A Valid Path");

            }
            else
            {
                //Test Purposes
                //String path = @"C:\Users\Jet\source\repos\3esiAssess\3esiAssess\Resource\csvdata.csv";

                //Assuming that the information will be imported to a database I temporarily put the data within a List of Entities

                StreamReader reader = new StreamReader(File.OpenRead(path));
                List<Well> wellList = new List<Well>();
                List<Group> groupList = new List<Group>();
                ArrayList errorLineNum = new ArrayList();

                int successCount = 0;
                int errorCount = 0;
                int lineCount = 0;
               


                /**Start of reading CSV**/

                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    string[] entityData = line.Split(',');
                    lineCount++;

                    string entityType = entityData[0];
                    Boolean isValid = true;

                    /**Defines whether an entity is a well or a group**/

                    switch (entityType.ToLower())
                    {
                        case "well":

                            /**Declared variables for the entityData for easy use.**/

                            String wellName = entityData[1];
                            int topX = Int32.Parse(entityData[2]);
                            int topY = Int32.Parse(entityData[3]);
                            int botX = Int32.Parse(entityData[4]);
                            int botY = Int32.Parse(entityData[5]);
                          
                            if (wellList.Count == 0)
                            {
                                Well newWell = new Well(wellName, topX, topY, botX, botY);
                                wellList.Add(newWell);
                                successCount++;

                            }
                            else
                            {   //Validattion againts business rules
                                foreach (Well well in wellList)
                                {
                                    if (well.Name.Equals(wellName) || well.TopHoleX == topX ||  well.BotHoleX == botX )
                                    {

                                        isValid = false;
                                        errorCount++;
                                        errorLineNum.Add(lineCount);
                                        break;

                                    }
                                }

                                if (isValid == true)
                                {
                                    Well newWell = new Well(wellName, topX, topY, botX, botY);
                                    wellList.Add(newWell);
                                    successCount++;

                                }
                              

                            }
                            break;


                        case "group":

                            String groupName = entityData[1];
                            int locX = Int32.Parse(entityData[2]);
                            int locY = Int32.Parse(entityData[3]);
                            int rad = Int32.Parse(entityData[4]);
                           


                            if (groupList.Count == 0)
                            {
                                Group newGroup = new Group(groupName, locX, locY, rad);
                                groupList.Add(newGroup);
                                successCount++;

                            }
                            else
                            {   /**Validattion againts business rules**/
                                foreach (Group group in groupList)
                                {
                                    int distX = group.LocationX - locX;
                                    int distY = group.LocationY - locY;

                                    double distance = Math.Sqrt(distX * distX + distY * distY);

                                    if (group.Name.Equals(groupName) || group.LocationX == locX || (distance < Math.Abs(group.Radius - rad)))
                                    {
                                        isValid = false;
                                        errorCount++;
                                        errorLineNum.Add(lineCount);
                                        break;

                                    }

                                }

                                if (isValid == true)
                                {
                                    Group newGroup = new Group(groupName, locX, locY, rad);
                                    groupList.Add(newGroup);
                                    successCount++;
                                }
                                

                            }
                            break;
                    }

                }


                /** Console Display*/
                Console.WriteLine(" --WELL -- \n");
                Console.WriteLine("Name" + "|" + "TopHoleX" + "|" + "TopHoleY" + "|" + "BotHoleX" + "|" + "BotHoleY");

                foreach (Well well in wellList)
                {
                    Console.WriteLine( well.Name + "|" + well.TopHoleX + "|" + well.TopHoleY + "|" + well.BotHoleX + "|" + well.BotHoleY);

                }

                Console.WriteLine(" ");
                Console.WriteLine(" --GROUP -- \n");
                Console.WriteLine("Name" + "|" + "LocationX" + "|" + "LocationY" + "|" + "Radius");

                foreach (Group group in groupList)
                {

                    Console.WriteLine(group.Name + "|" + group.LocationX + "|" + group.LocationY + "|" + group.Radius);

                }

                Console.WriteLine(" ");
                Console.WriteLine("--- Import Summary ---\n");


                Console.WriteLine("CSV File Error Found: " + errorCount);

                foreach (int i in errorLineNum)
                {

                    Console.WriteLine("Line Number: " + i);
                }

                Console.WriteLine(" ");
                Console.WriteLine("Successfully Imported: " + successCount + " Items");

             }
            Console.ReadKey();
            }

        }
    }
