using System;
using System.Linq;
using Application.Interfaces;
using AutoMapper;
using Domain;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.ViewModels.ActivityDto
{

    public class MonthDiffResolver : IValueResolver<Activity, ActivityItem, int>
    {
        public int Resolve(Activity source, ActivityItem destination, int destMember, ResolutionContext context)
        {
            var current = DateTime.Now;
            var dateDiff = ((source.HoldingDate.Year - current.Year) * 12) + (source.HoldingDate.Month - current.Month);
            return dateDiff;

        }
    }
}