﻿namespace Garage;

public class Garage
{
    public ParkingSpot[] ParkingSpots { get; } = new ParkingSpot[50];

    bool IsOccupied(int parkingSpotNumber)
    {
        if (ParkingSpots[parkingSpotNumber] is null) return true;
        return false;
    }

    
    public bool TryOccupy(int parkingSpotNumber, string licensePlate, DateTime entryTime)
    {
        if (IsOccupied(parkingSpotNumber))
        {
            ParkingSpots[parkingSpotNumber] = new ParkingSpot
            {
                licensePlate = licensePlate,
                entryData = entryTime
            };
            return true;
        }
        Console.WriteLine("The parking spot is occupied");
        return false;
    }
    public bool TryExit(int parkingSpotNumber, DateTime exitTime, ref decimal costs)
    {
        if(!IsOccupied(parkingSpotNumber))
        {
            int min = CalculateMinutes(exitTime) - CalculateMinutes(ParkingSpots[parkingSpotNumber].entryData);
            if(min >= 15)
            {
                costs = min/30 *3;
            }
            ParkingSpots[parkingSpotNumber] = null!;
            Console.WriteLine($"You have to pay: {costs}");
            return true;
        }
        Console.WriteLine("The parking spot isn't occupied");
        return false;
    }
    static int CalculateMinutes(DateTime time)
    {
        int min = time.Minute;
        min += time.Day * 24 * 60;
        min += time.Hour * 60;
        return min;
    }
    public void GenerateReport()
    {
        Console.WriteLine("Spot     License Plate \n----------------------");
        for(int i = 0; i < ParkingSpots.Length; i++)
        {
            Console.WriteLine($"{i+1,-4}  |  {ParkingSpots[i]?.licensePlate}");
        }
    }
}