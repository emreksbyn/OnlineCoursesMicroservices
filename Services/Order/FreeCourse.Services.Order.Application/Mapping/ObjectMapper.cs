using AutoMapper;

namespace FreeCourse.Services.Order.Application.Mapping
{
    //  Normalde static ise  uygulama ayaga kalkarken initialize edilir.
    public static class ObjectMapper
    {
        // Sadece istenildigi anda initialize edilsin istiyorsak Lazy kullanilir.
        private static readonly Lazy<IMapper> lazy = new Lazy<IMapper>(() =>
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<CustomMapping>();
            });

            return config.CreateMapper();
        });

        public static IMapper Mapper => lazy.Value;
    }
}