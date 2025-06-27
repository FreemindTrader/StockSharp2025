//using System;
//using System.Linq;
//using System.ComponentModel;
//using DevExpress.Mvvm;
//using DevExpress.Mvvm.POCO;
//using DevExpress.Mvvm.DataAnnotations;
//using fx.Database.Common.Utils;
//using fx.Database.Common.DataModel;

//namespace fx.Database.Common.ViewModel
//{
//    public abstract class DocumentsViewModel<TModule, TUnitOfWork>
//        where TModule : ModuleDescription<TModule>
//        where TUnitOfWork : IUnitOfWork
//    {

//        readonly Func<IUnitOfWorkFactory<TUnitOfWork>> getUnitOfWorkFactory;

//        protected DocumentsViewModel(Func<IUnitOfWorkFactory<TUnitOfWork>> getUnitOfWorkFactory)
//        {
//            this.getUnitOfWorkFactory = getUnitOfWorkFactory;
//            Modules = CreateModules().ToArray();
//            foreach (var module in Modules)
//                Messenger.Default.Register<NavigateMessage<TModule>>(this, module, x => Show(x.Token));

//        }

//        protected IDocumentManagerService DocumentManagerService { get { return this.GetService<IDocumentManagerService>(); } }

//        protected IDocumentManagerService WorkspaceDocumentManagerService { get { return this.GetService<IDocumentManagerService>("WorkspaceDocumentManagerService"); } }

//        public TModule[] Modules { get; private set; }

//        protected virtual TModule DefaultModule { get { return Modules.First(); } }

//        public virtual TModule ActiveModule { get; protected set; }

//        public void SaveAll()
//        {
//            Messenger.Default.Send(new SaveAllMessage());
//        }

//        public void OnClosing(CancelEventArgs cancelEventArgs)
//        {
//            Messenger.Default.Send(new CloseAllMessage(cancelEventArgs));
//        }

//        public void Show(TModule module)
//        {
//            if (module == null || DocumentManagerService == null)
//                return;
//            IDocument document = DocumentManagerService.FindDocumentByIdOrCreate(module, x => CreateDocument(module));
//            ShowDocument(module, document);
//        }

//        public virtual void OnLoaded()
//        {
//            DocumentManagerService.ActiveDocumentChanged += OnActiveDocumentChanged;
//            Show(DefaultModule);
//        }

//        void OnActiveDocumentChanged(object sender, ActiveDocumentChangedEventArgs e)
//        {
//            ActiveModule = e.NewDocument.Id as TModule;
//        }

//        protected virtual void OnActiveModuleChanged(TModule oldModule) { }

//        protected virtual void ShowDocument(TModule module, IDocument document)
//        {
//            document.Show();
//        }

//        IDocument CreateDocument(TModule module)
//        {
//            var document = DocumentManagerService.CreateDocument(module.DocumentType, null, this);
//            document.Title = GetModuleTitle(module);
//            document.DestroyOnClose = false;
//            return document;
//        }

//        protected virtual string GetModuleTitle(TModule module)
//        {
//            return module.ModuleTitle;
//        }

//        public void PinPeekCollectionView(TModule module)
//        {
//            if (WorkspaceDocumentManagerService == null)
//                return;
//            IDocument document = WorkspaceDocumentManagerService.FindDocumentByIdOrCreate(this, x => CreatePinnedPeekCollectionDocument(module));
//            document.Show();
//        }

//        IDocument CreatePinnedPeekCollectionDocument(TModule module)
//        {
//            var document = WorkspaceDocumentManagerService.CreateDocument("PeekCollectionView", module.CreatePeekCollectionViewModel());
//            document.Title = module.ModuleTitle;
//            return document;
//        }

//        protected object GetPeekCollectionViewModelFactory<TEntity, TPrimaryKey>(TModule module, Func<TUnitOfWork, IRepository<TEntity, TPrimaryKey>> getRepositoryFunc) where TEntity : class
//        {
//            var viewModel = PeekCollectionViewModel<TModule, TEntity, TPrimaryKey, TUnitOfWork>.Create(module, getUnitOfWorkFactory(), getRepositoryFunc);
//            viewModel.SetParentViewModel(this);
//            return viewModel;
//        }

//        protected abstract TModule[] CreateModules();
//    }

//    public abstract partial class ModuleDescription<TModule> where TModule : ModuleDescription<TModule>
//    {
//        Func<TModule, object> peekCollectionViewModelFactory;

//        object peekCollectionViewModel;

//        protected ModuleDescription(string title, string documentType, string group, Func<TModule, object> peekCollectionViewModelFactory)
//        {
//            ModuleTitle = title;
//            ModuleGroup = group;
//            DocumentType = documentType;
//            this.peekCollectionViewModelFactory = peekCollectionViewModelFactory;
//        }

//        public string ModuleTitle { get; private set; }

//        public string ModuleGroup { get; private set; }

//        public string DocumentType { get; private set; }

//        public object PeekCollectionViewModel
//        {
//            get
//            {
//                if (peekCollectionViewModelFactory == null)
//                    return null;
//                if (peekCollectionViewModel == null)
//                    peekCollectionViewModel = CreatePeekCollectionViewModel();
//                return peekCollectionViewModel;
//            }
//        }

//        public object CreatePeekCollectionViewModel()
//        {
//            return peekCollectionViewModelFactory((TModule)this);
//        }
//    }
//}