using Bellatrix.DataGeneration.Configuration;
using Bogus;

namespace Bellatrix.DataGeneration.Utilities;

public static class FakerFactory
{
    private static readonly object _lock = new();
    private static Faker _sharedFaker;
    private static int? _seed;
    private static string _locale;

    /// <summary>
    /// Initializes the FakerFactory with a global seed (optional).
    /// Should be called once during test initialization.
    /// </summary>
    public static void Initialize(string locale = null, int? seed = null)
    {
        lock (_lock)
        {
            _locale = locale ?? Settings.GetSection<TestValueGenerationSettings>().Locale;
            _seed = seed ?? Settings.GetSection<TestValueGenerationSettings>().Seed;

            if (_seed.HasValue)
            {
                Randomizer.Seed = new Random(_seed.Value);
            }

            _sharedFaker = new Faker(locale: _locale);
        }
    }

    /// <summary>
    /// Gets the globally shared Faker instance.
    /// </summary>
    public static Faker GetFaker()
    {
        if (_sharedFaker == null)
        {
            // Default to unseeded, but allow lazy fallback
            Initialize();
        }

        return _sharedFaker;
    }

    /// <summary>
    /// Creates a new independent Faker (optionally with seed).
    /// </summary>
    public static Faker CreateNew(string locale = null, int? seed = null)
    {
       
        if (_seed.HasValue)
        {
            _locale = locale ?? Settings.GetSection<TestValueGenerationSettings>().Locale;
            _seed = seed ?? Settings.GetSection<TestValueGenerationSettings>().Seed;
            Randomizer.Seed = new Random(_seed.Value);
        }

        var newFaker = new Faker(locale: _locale);

        return newFaker;
    }
}