//using System.Data.Entity.ModelConfiguration;
//using Microsoft.EntityFrameworkCore;
//using MoviesAPI.Domain.Entities;

//namespace MoviesAPI.Persistence.Configuration
//{
//    public class MovieConfiguration : EntityTypeConfiguration<Movie>
//    {
//        public MovieConfiguration()
//        {
//            this.HasKey(g => g.Id);
//            Property(m => m.Title)
//                .IsRequired()
//                .HasMaxLength(300);
//        }
//    }
//}