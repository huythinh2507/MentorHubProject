using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YPP.MH.DataAccessLayer.Models;
using static Org.BouncyCastle.Math.EC.ECCurve;
using YPP.MH.DataAccessLayer.Repositories.Base;
using YPP.MH.DigitalAssetManagement;
using YPP.MH.BusinessLogicLayer.DTOs;

namespace YPP.MH.BusinessLogicLayer.Services.CourseManagement
{
    public class CourseService
    {
        private readonly IDbContextFactory<MHDbContext> _dbContextFactory;

        public CourseService(IDbContextFactory<MHDbContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task<Course> CreateCourseAsync(CreateCourseDto dto)
        {
            await using var context = await _dbContextFactory.CreateDbContextAsync();
            var course = new Course
            {
                Type = dto.CourseType.ToString(),
                Name = dto.Name,
                Icon = dto.Icon,
                SpaceGroupId = dto.SpaceId,
                IsAddAll = dto.IsAddAll,
            };

            context.Course.Add(course);
            await context.SaveChangesAsync();

            return course;
        }

        public async Task<Course> GetCourseByIdAsync(int id)
        {
            await using var context = await _dbContextFactory.CreateDbContextAsync();
            var course = await context.Course
                               .Include(c => c.CourseCategory)
                               .Include(c => c.Space)
                               .FirstOrDefaultAsync(c => c.Id == id);

            ArgumentNullException.ThrowIfNull(course);

            return course;
        }

        public async Task<List<Course>> GetCourses()
        {
            await using var context = await _dbContextFactory.CreateDbContextAsync();
            var courses = await context.Course
                               .Include(c => c.CourseCategory)
                               .Include(c => c.Space)
                               .ToListAsync();

            ArgumentNullException.ThrowIfNull(courses);

            return courses;
        }
    }
}
