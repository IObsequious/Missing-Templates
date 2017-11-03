namespace $rootnamespace$
{
    public static class $safeitemrootname$
    {
        public static $fileinputname$Infrastructure Create()
        {
          $fileinputname$ model = new $fileinputname$();
          $fileinputname$ViewModel viewModel = new $fileinputname$ViewModel(model);
          $fileinputname$View view = new $fileinputname$View(viewModel);
          return new $fileinputname$Infrastructure(model, viewModel, view);
        }
    }
}
