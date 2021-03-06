﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net;
using System.Web;

namespace Angular_Demo_Complete.Entities
{
    public class Album
    {

        public Album() {

        }

        public Album(String url) {
            downloadImage(url);
        }

        [Key]
        public int ID { get; set; }

        public virtual Artist Owner { get; set; }

        public String title { get; set; }

        public int views { get; set; }

        public String FilePath { get; set; }

        public Boolean canDownload { get; set; }

        public virtual List<Song> Songs { get; set; } = new List<Song>();

        public byte[] image { get; set; }

        public String imageLink { get; set; }

        [NotMapped]
        public double price { get {
                return calculatePrice();
            } set {
                this.price = value;
            } }
        
        [NotMapped]
        public String ArtistName { get {
                return this.Owner.firstName;
            } set {
                ArtistName = value;
            } }
        
        public bool downloadImage(String url) {
            var client = new WebClient();

            try
            {
                var data = client.DownloadData(url);
                this.image = data;
                return true;
            }
            catch (Exception)
            {
                return false;
            }


        }

        public double calculatePrice() {
            var total = 0.0;


            foreach (var s in Songs) {
                total += s.price;
            }

            if (Songs.Count > 1) {
                total = (total * .6);
            }

            return Math.Round(total, 2);
        }
        
    }
}