using Xunit;

namespace ERP_System_Test
{
    public class MakeOneTestCaseWithErrorToAllowEMailNotificationcs
    {
        [Fact]
        public void MakeOnMakeOneTestCaseWithErrorToAllowEMailNotificationcs()
        {
            // Denne test fejler med vilje, så CI altid rapporterer mindst én fejl.
            // Dermed får man altid sendt en mail tilbage, når
            // man har lavet en PUSH => CI/CD af sit projekt. 
            // Hvis dette er den ENESTE fejl, er alt OK!
            //Assert.True(false, "Denne test fejler altid med vilje. Se README for forklaring. Testen er lavet så, der altid bliver sendt en mail tilbage efter PUSH => CI/CD");
        }
    }
}
