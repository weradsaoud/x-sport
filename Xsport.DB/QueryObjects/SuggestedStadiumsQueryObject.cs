using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xsport.Common.Enums;
using Xsport.DB.Entities;
using Xsport.DTOs.StadiumDtos;

namespace Xsport.DB.QueryObjects
{
    public static class SuggestedStadiumsQueryObject
    {
        public static IQueryable<SuggestedStadiumDto> MapStadiumToSuggested(
            this IQueryable<Stadium> stadiums, short currentLanguageId, string domainName)
        {
            try
            {
                IQueryable<SuggestedStadiumDto> suggestedStadiums = stadiums.Select(stadium => new SuggestedStadiumDto
                {
                    StadiumId = stadium.StadiumId,
                    StadiumName = stadium.StadiumTranslations.Single(t => t.LanguageId == currentLanguageId).Name,
                    StadiumType = stadium.Floor.FloorTranslations
                    .Single(t => t.LanguageId == currentLanguageId).Name,
                    NumOfReviews = stadium.StadiumReviews.Count(),
                    Evaluation = stadium.StadiumReviews.Count() > 0 ?
                    stadium.StadiumReviews.Select(r => r.Evaluation).Average() : 0,
                    Lat = stadium.Latitude,
                    Long = stadium.Longitude,
                    //TODO region name
                    CoverPhoto = string.IsNullOrEmpty(stadium.Mutimedias
                    .Single(m => m.IsCover == true && m.IsVideo == false).FilePath)
                    ? "" : domainName + "/Images/" + stadium.Mutimedias
                    .Single(m => m.IsCover == true && m.IsVideo == false).FilePath,
                    CoverVideo = string.IsNullOrEmpty(stadium.Mutimedias
                    .Single(m => m.IsCover == true && m.IsVideo == true).FilePath)
                    ? "" : domainName + "/Images/" + stadium.Mutimedias
                    .Single(m => m.IsCover == true && m.IsVideo == true).FilePath,
                    Photos = stadium.Mutimedias.Where(m => m.IsCover == false && m.IsVideo == false)
                    .Select(m => string.IsNullOrEmpty(m.FilePath) ? "" : domainName + "/Images/" + m.FilePath).ToList(),
                    Videos = stadium.Mutimedias.Where(m => m.IsCover == false && m.IsVideo == true)
                    .Select(m => string.IsNullOrEmpty(m.FilePath) ? "" : domainName + "/Images/" + m.FilePath).ToList(),
                });
                return suggestedStadiums;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static IQueryable<SuggestedStadiumDto> OrderSuggestedStadiums(
            this IQueryable<SuggestedStadiumDto> stadiums, SuggestedStadiumsOrderOptions orderOptions)
        {
            try
            {
                switch (orderOptions)
                {
                    case SuggestedStadiumsOrderOptions.SimpleOrder:
                        return stadiums.OrderBy(s => s.StadiumId);
                    case SuggestedStadiumsOrderOptions.NameAsc:
                        return stadiums.OrderBy(s => s.StadiumName);
                    case SuggestedStadiumsOrderOptions.NameDesc:
                        return stadiums.OrderByDescending(s => s.StadiumName);
                    case SuggestedStadiumsOrderOptions.NumOfReviewsUp:
                        return stadiums.OrderBy(s => s.NumOfReviews);
                    case SuggestedStadiumsOrderOptions.NumOfReviewsDown:
                        return stadiums.OrderByDescending(s => s.NumOfReviews);
                    case SuggestedStadiumsOrderOptions.EvaluationUp:
                        return stadiums.OrderBy(s => s.Evaluation);
                    case SuggestedStadiumsOrderOptions.EvaluationDown:
                        return stadiums.OrderByDescending(s => s.Evaluation);
                    default:
                        throw new ArgumentOutOfRangeException(
                        nameof(orderOptions), orderOptions, null);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static IQueryable<SuggestedStadiumDto> FilterSuggestedStadiums(
            this IQueryable<SuggestedStadiumDto> stadiums,
            SuggestedStadiumsFilterOptions filterOption, string filterValue)
        {
            try
            {
                switch (filterOption)
                {
                    case SuggestedStadiumsFilterOptions.ByName:
                        return stadiums.Where(s => s.StadiumName.Contains(filterValue));
                    case SuggestedStadiumsFilterOptions.ByEvaluationUp:
                        double evalUp = double.Parse(filterValue);
                        return stadiums.Where(s => s.Evaluation >= evalUp);
                    case SuggestedStadiumsFilterOptions.ByEvaluationDown:
                        double evalDown = double.Parse(filterValue);
                        return stadiums.Where(s => s.Evaluation <= evalDown);
                    case SuggestedStadiumsFilterOptions.ByEvaluation:
                        double eval = double.Parse(filterValue);
                        return stadiums.Where(s => s.Evaluation == eval);
                    case SuggestedStadiumsFilterOptions.NumOfReviewsUp:
                        int numRevUp = int.Parse(filterValue);
                        return stadiums.Where(s => s.NumOfReviews >= numRevUp);
                    case SuggestedStadiumsFilterOptions.NumOfReviewsDown:
                        int numRevDown = int.Parse(filterValue);
                        return stadiums.Where(s => s.NumOfReviews <= numRevDown);
                    case SuggestedStadiumsFilterOptions.NumOfReviews:
                        int numRev = int.Parse(filterValue);
                        return stadiums.Where(s => s.NumOfReviews <= numRev);
                    default:
                        throw new ArgumentOutOfRangeException(
                        nameof(filterOption), filterOption, null);
                }
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
    }
}
