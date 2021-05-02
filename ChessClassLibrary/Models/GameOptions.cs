using ChessClassLibrary.enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessClassLibrary.Models
{
    public class GameOptions
    {
        public string Player1 { get; set; }
        public string Player2 { get; set; }

        [Required]
        public GameVarient GameVarient { get; set; }

        [Range(0, 600), Required]
        public int SecondsPerSide { get; set; }

        [Range(1, 600), Required]
        public int IncrementInSeconds { get; set; }

        [Required]
        public PieceColor Side { get; set; }
    }
}
