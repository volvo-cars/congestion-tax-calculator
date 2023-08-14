using congestion.Model;
using System.Text;
using System.Threading.Tasks;

namespace congestion.Model
{
    public class Car : Vehicle
    {
        public bool IsTaxExempt()
        {
            return false;
        }
    }
}