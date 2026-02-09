using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Conectar.Helpers
{
    public static class AudioManager
    {
        // Usamos MediaPlayer porque permite controlar volumen de forma independiente
        private static readonly MediaPlayer _player = new MediaPlayer();
        private static double _lastVolume = 0.5; // Para recordar el volumen antes de silenciar
        private static bool _isMuted = false;

        public static void StartMusic(string fileName)
        {
            string path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", fileName);
            _player.Open(new Uri(path));

            // Evento para que la música se repita (Loop)
            _player.MediaEnded += (s, e) => {
                _player.Position = TimeSpan.Zero;
                _player.Play();
            };

            _player.Play();
            _player.Volume = 0.5; // Volumen inicial al 50%
        }

        public static void SetVolume(double volume)
        {
            // Solo actualizamos el volumen si no está silenciado
            // O si el usuario mueve el slider, entendemos que quiere quitar el silencio
            _player.Volume = volume;
            if (volume > 0) _isMuted = false;
        }

        public static double GetVolume() => _player.Volume;

        // --- NUEVA LÓGICA DE MUTE ---

        public static bool IsMuted() => _isMuted;

        public static bool ToggleMute()
        {
            if (!_isMuted)
            {
                _lastVolume = _player.Volume; // Guardamos el volumen actual
                _player.Volume = 0;           // Silenciamos
                _isMuted = true;
            }
            else
            {
                _player.Volume = _lastVolume; // Restauramos el volumen previo
                _isMuted = false;
            }
            return _isMuted;
        }
    }
}