using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Skyblock.Logic.Models
{
    public class NotifyPropertyChanged : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        // ref sonst kopie und dann könnt ma nix zuweisen falls kein Objekt
        // Attribut: Compiler setzt Membername vom Caller (aufrufendes Property) ein, falls nix drinnen
        public void Set<T>(ref T field, T value, [CallerMemberName]string propertyName = "")
        {
            // nur bei Änderungen sonst immer rerendering!
            if (!EqualityComparer<T>.Default.Equals(value, field))
            {
                field = value;
                this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
