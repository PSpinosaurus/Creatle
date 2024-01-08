using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer
{
    public static class ContextHelper
    {
        private static CreatleDbContext creatleDbContext;
        private static AnswerContext answerContext;
        private static CategoriesContext categoriesContext;
        private static CategoriesValuesContext categoriesValuesContext;
        private static GameContext gameContext;
        private static HeroMetadataContext heroMetadataContext;
        private static HeroProfileContext heroProfileContext;

        public static CreatleDbContext GetCreatleDbContext()
        {
            if (creatleDbContext == null)
            {
                SetCreatleDbContext();
            }
            
            return creatleDbContext;
        }

        public static void SetCreatleDbContext()
        {
            creatleDbContext = new CreatleDbContext();
        }

        public static AnswerContext GetAnswerContext()
        {
            if (answerContext == null)
            {
                SetAnswerContext();
            }

            return answerContext;
        }

        public static void SetAnswerContext()
        {
            answerContext = new AnswerContext(GetCreatleDbContext());
        }

        public static CategoriesContext GetCategoriesContext()
        {
            if (categoriesContext == null)
            {
                SetCategoriesContext();
            }

            return categoriesContext;
        }

        public static void SetCategoriesContext()
        {
            categoriesContext = new CategoriesContext(GetCreatleDbContext());
        }

        public static CategoriesValuesContext GetCategoriesValuesContext()
        {
            if(categoriesValuesContext == null)
            {
                SetCategoriesValuesContext();
            }

            return categoriesValuesContext;
        }

        public static void SetCategoriesValuesContext()
        {
            categoriesValuesContext = new CategoriesValuesContext(GetCreatleDbContext());
        }

        public static GameContext GetGameContext()
        {
            if(gameContext  == null)
            {
                SetGameContext();
            }

            return gameContext;
        }

        public static void SetGameContext()
        {
            gameContext = new GameContext(GetCreatleDbContext());
        }

        public static HeroMetadataContext GetHeroMetadataContext()
        {
            if(heroMetadataContext == null)
            {
                SetHeroMetadataContext();
            }

            return heroMetadataContext;
        }

        public static void SetHeroMetadataContext() 
        {
            heroMetadataContext = new HeroMetadataContext(GetCreatleDbContext());
        }

        public static HeroProfileContext GetHeroProfileContext()
        {
            if(heroProfileContext == null)
            {
                SetHeroProfileContext();
            }

            return heroProfileContext;
        }

        public static void SetHeroProfileContext()
        {
            heroProfileContext = new HeroProfileContext(GetCreatleDbContext());
        }
    }
}
