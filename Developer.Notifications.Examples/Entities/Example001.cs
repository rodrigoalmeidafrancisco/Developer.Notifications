using Developer.Notifications.Validations;

namespace Developer.Notifications.Examples.Entities
{
    public class Example001 : Notifiable<Notification>
    {
        public Example001()
        {

        }

        #region Properties

        public int ValorInt { get; set; }
        public long ValorLong { get; set; }

        #endregion Properties

        #region Methods of validation

        public void Validar01()
        {
            if (ValorInt < 0)
                AddNotification(nameof(ValorInt), "O valor do campo ValorInt não pode ser negativo.");
            if (ValorLong < 0)
                AddNotification(nameof(ValorLong), "O valor do campo ValorLong não pode ser negativo.");
        }

        public void Validar02()
        {
            if (ValorInt < 0)
                AddNotification("O valor do campo ValorInt não pode ser negativo.");
            if (ValorLong < 0)
                AddNotification("O valor do campo ValorLong não pode ser negativo.");
        }

        public void Validar03()
        {
            AddNotifications(new Contract<Example001>().Requires()
                 .IsLowerThan(ValorInt, 0, nameof(ValorInt), "O valor do campo ValorInt não pode ser negativo.")
                 .IsLowerThan(ValorLong, 0, nameof(ValorLong), "O valor do campo ValorLong não pode ser negativo.")
             );
        }

        public void Validar04()
        {
            AddNotifications(new Contract<Example001>().Requires()
                 .IsLowerThan(ValorInt, 0, "O valor do campo ValorInt não pode ser negativo.")
                 .IsLowerThan(ValorLong, 0, "O valor do campo ValorLong não pode ser negativo.")
             );
        }

        #endregion Methods of validation
    }
}
