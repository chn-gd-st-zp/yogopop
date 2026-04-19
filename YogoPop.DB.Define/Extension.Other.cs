namespace YogoPop.DB.Define;


public delegate TOptions OptionsBuilderAction<TOptions>(TOptions options);


public delegate TOOptions OptionsBuilderAction<TIOptions, TOOptions>(TIOptions options);