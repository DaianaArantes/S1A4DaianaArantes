/*
 * Program ID: S1A4DaianaArantes - Assignment 4 
 * 
 * Programming Concepts - 1 Semester
 * 
 * Purpose: Create a C# Console Application project to reserve seats
 * in a Theatre, also delete reservation
 * 
 * Revised History
 * 
 * Written Jan 2018 by Daiana Arantes
 */




using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment04_v2
{
    class Program
    {

        //declarations
        static int amountOfColumns = 4;
        static int amountOfLines = 4;
        static int menuOption = 0;
        static int selectedRow = 0;
        static int selectedColumn = 0;
        static int positionSelected;
        static int seatPosition = 0;
        static string name = null;
        static int removeOption = 0;
        static string[] seats = new string[amountOfColumns * amountOfLines];

        //method to print chart
        public static void DisplaySeats()
        {
            for (int line = 0; line < amountOfLines; line++)
            {
                for (int column = 0; column < amountOfColumns; column++)
                {
                    seatPosition = ((line * amountOfLines) + column);

                    if (seats[seatPosition] == null)
                    {
                        // print with the "Seat" when the user chooses a seat
                        Console.Write((line + 1) + "-" + (column + 1) +
                            " Seat ");
                    }
                    else
                    {
                        // print without the "Seat" when the user chooses a seat
                        Console.Write((line + 1) + "-" + (column + 1) +
                            " " + seats[seatPosition]);
                    }
                }
                Console.WriteLine();
            }
        }

        public static bool AreSeatsAvailable()
        {
            for (int i = 0; i < seats.Length; i++)
            {
                if (seats[i] == null)
                {
                    return true;
                }
            }
            return false;
        }

        public static void RemoveReservation()
        {
            do
            {
                //case 2 to remove revervation either by name or seat position
                Console.WriteLine("\nPress 1 to remove using your name\n");
                Console.WriteLine("Press 2 to remove using your seat number\n");
                Console.WriteLine("Press 3 to return to main menu\n");
                removeOption = Program.ReceiveValue();


                switch (removeOption)
                {

                    case 1:
                        //case 1 remove reservation by user name

                        Console.Write("\nPlease insert your name: \n");
                        name = Console.ReadLine();
                        bool reservationRemoved = false;

                        for (int i = 0; i < seats.Length; i++)
                        {
                            if (seats[i] == name)
                            {
                                reservationRemoved = true;
                                seats[i] = null;
                            }
                        }
                        //validates if seat is not empty
                        if (reservationRemoved)
                        {
                            Console.Clear();
                            Console.WriteLine("\nYour reservation was" +
                                " removed!\n");
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("\nThere is no reservation" +
                                " for your name!\n");
                        }

                        DisplaySeats();
                        break;

                    case 2:
                        Console.Clear();
                        //case to remome reservation by seat position
                        Console.WriteLine("\nPlease insert the line of" +
                            " your seat\n");
                        selectedRow = Program.ReceiveValue();
                        Console.Clear();
                        Console.WriteLine("\nYou selected line " + selectedRow +
                            "!. Now Please insert the colunm of your seat\n");
                        selectedColumn = Program.ReceiveValue();

                        try
                        {
                            //calculation to remove a user from a seat 
                            //position set to null
                            positionSelected = (((selectedRow - 1) *
                                amountOfColumns)
                            + (selectedColumn)) - 1;

                            if (seats[positionSelected] != null)
                            {
                                Console.Clear();
                                seats[positionSelected] = null;
                                Console.WriteLine("\nYour reservation was" +
                                    " removed!\n");
                                removeOption = 3;
                            }
                            else
                            {
                                Console.Clear();
                                Console.WriteLine("There is no reservation" +
                                    " in this seat, please try again!\n");
                            }
                            DisplaySeats();

                        }
                        catch (IndexOutOfRangeException ex)

                        {
                            Console.WriteLine("\nThis seat does not exist!\n");
                        }
                        break;

                    case 3:
                        Console.Clear();
                        Console.WriteLine("You returned to the main menu!");
                        break;
                    default:
                        Console.WriteLine();
                        Console.WriteLine("\nInvalid option," +
                            "please input 1, 2: \n");
                        break;
                }

            } while (removeOption != 3);
        }

        public static int ReceiveValue()
        {
            int value = 0;
            bool valueReceived = false;

            do
            {
                //try to execute parse
                try
                {
                    value = int.Parse(Console.ReadLine());
                    valueReceived = true;
                }
                //catch null character exception from user
                catch (ArgumentNullException ex)
                {
                    Console.WriteLine();
                    valueReceived = false;
                    Console.WriteLine("Your insert is a null value," +
                        "please insert a valid number!");
                }
                //catch wrong character exception from user
                catch (FormatException ex)
                {
                    Console.WriteLine();
                    valueReceived = false;
                    Console.WriteLine("You inserted an invalid character," +
                        " please insert a valid number!");
                }
                //catch overflow exception from user
                catch (OverflowException ex)
                {
                    Console.WriteLine();
                    valueReceived = false;
                    Console.WriteLine("You inserted an unsupported" +
                        " value, please" +
                        "insert a number between " + int.MinValue +
                        " and " + int.MaxValue);
                }
                //catch other exceptions 
                catch (Exception ex)
                {
                    valueReceived = false;
                    Console.WriteLine(ex.Message + ". Please insert a" +
                        " valid number!");
                }

            } while (!valueReceived);

            return value;
        }

        public static void Main(string[] args)
        {

            Console.WriteLine("\nWelcome to the Theatre!\n");

            do
            {
                //main menu
                Console.WriteLine("\nPlease select an option from the menu:\n");
                Console.WriteLine("\nInsert 1 to reserve a seat\n");
                Console.WriteLine("Insert 2 to remove a reservation\n");
                Console.WriteLine("Insert 3 to exit");
                Console.WriteLine();
                menuOption = Program.ReceiveValue();
                Console.Clear();


                switch (menuOption)
                {

                    case 1:

                        //case 1 reserve a seat by user name

                        bool keepGoing = true;
                        int number = 0;
                        do
                        {

                            Console.WriteLine("\nPlease insert your name:\n");
                            name = Console.ReadLine();

                            if (string.IsNullOrEmpty(name))
                            {
                                Console.WriteLine("Imput is empty or null!");
                            }
                            else if (name.Length < 2)
                            {
                                Console.WriteLine("Imput too short!");
                            }
                            else if (int.TryParse(name, out number))
                            {
                                Console.WriteLine("Your input is numeric," +
                                    " please insert a valid name!");
                            }
                            else
                            {
                                keepGoing = false;
                            }
                        } while (keepGoing);

                        if (AreSeatsAvailable())
                        {
                            Console.Clear();
                            Console.WriteLine(name + " These are the Seats" +
                                " available\n");
                            DisplaySeats();
                            Console.WriteLine("\nPlease insert the line you" +
                                " want to seat\n");
                            selectedRow = Program.ReceiveValue();
                            Console.Clear();
                            Console.WriteLine("\nYou selected line " +
                                selectedRow + "!. Now Please insert the" +
                                " colunm you want to seat\n");
                            selectedColumn = Program.ReceiveValue();
                            Console.Clear();


                            //calculation to reserve a seat
                            try
                            {
                                //validate if position exists
                                if (selectedRow > amountOfLines ||
                                    selectedColumn > amountOfColumns)
                                {
                                    throw new IndexOutOfRangeException();
                                }

                                positionSelected = (((selectedRow - 1) *
                                    amountOfColumns)
                                    + (selectedColumn)) - 1;

                                //validate if the seat is already taken
                                if (seats[positionSelected] != null)
                                {
                                    Console.WriteLine("This seat is already" +
                                        " taken, please choose another one!");
                                }
                                else
                                {
                                    seats[positionSelected] = name;
                                    Console.WriteLine(name +
                                        " Your reservation was made!\n");
                                    DisplaySeats();
                                }
                            }
                            catch (IndexOutOfRangeException ex)
                            {
                                Console.WriteLine("\nThis seat does" +
                                    " not exist!");
                            }
                        }
                        else
                        {
                            Console.WriteLine("\nThere is no seat " +
                                "available, sorry");
                        }
                        break;

                    case 2:
                        Program.RemoveReservation();
                        break;

                    case 3:
                        //case 3 to exit the program
                        System.Environment.Exit(0);
                        break;

                    default:
                        Console.WriteLine();
                        Console.WriteLine("Invalid option," +
                            "please input 1, 2 or 3: ");
                        break;
                }
            } while (menuOption != 1 || menuOption != 2 || menuOption != 3);
        }

    }
}
