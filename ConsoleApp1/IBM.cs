using IBM.Data.Informix;

namespace ConsoleApp1
{
    internal class IBM
    {
        public void Conn()
        {
            using (IfxConnection connection = new IfxConnection())
            {
                string sqlcmd = "";
                using (IfxCommand cmd = new IfxCommand())
                {
                }
            }
        }
    }
}