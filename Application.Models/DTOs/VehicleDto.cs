namespace minimal_api.Application.Models.DTOs;

public abstract record VehicleDto
{
    protected VehicleDto() { }
    
    protected VehicleDto(string name, string brand, int year)
    {
        Name = name;
        Brand = brand;
        Year = year;
    }

    public string Name { get; set; }
    public string Brand { get; set; }
    public int Year { get; set; }
}