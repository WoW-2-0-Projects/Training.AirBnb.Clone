namespace Backend_Project.Application.Reservations;

public class ReservationOccupancySettings
{
    public int MinAdults { get; set; }

    public int MaxAdults { get; set; }

    public int MinChildren { get; set; }

    public int MaxChildren { get; set; }

    public int MaxInfants { get; set;}

    public int MinInfants { get; set; }

    public int MinPets { get; set; }

    public int MaxPets { get; set;}

    public int MinReservationTotalPrice { get; set; }
}