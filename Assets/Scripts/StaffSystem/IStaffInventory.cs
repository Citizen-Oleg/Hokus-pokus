using PlayerComponent;

namespace StaffSystem
{
    public interface IStaffInventory : IStaff
    {
        public Inventory Inventory { get; }
    }
}