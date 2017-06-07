using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArcGIS.Desktop.Framework;
using ArcGIS.Desktop.Framework.Contracts;
using System.Windows;
using ArcGIS.Desktop.Mapping;
using ArcGIS.Desktop.Core;
using System.ComponentModel;
using ArcGIS.Desktop.Core.Events;
using ArcGIS.Desktop.Framework.Threading.Tasks;
using ArcGIS.Core.Data;
using ArcGIS.Core.Geometry;
using ArcGIS.Desktop.Layouts;
using System.Collections.Concurrent;
using System.Xml;
using ArcGIS.Core.CIM;
using System.Windows.Controls;

namespace ProAppModule1
{
    internal class Dockpane1ViewModel : DockPane
    {
        private const string _layoutName = "Neighborhood_Stabilization";
        private const string _layerName = "OH_Blocks";

        private const string _dockPaneID = "ProAppModule1_Dockpane1";

        private Object thisLock;

        protected Dockpane1ViewModel()
        {
            TaskScheduler.UnobservedTaskException += OnUnobservedTaskException;

            ProjectOpenedEvent.Subscribe(new Action<ProjectEventArgs>((e) => { OnProjectOpened(e); } ));
            ProjectItemsChangedEvent.Subscribe(new Action<ProjectItemsChangedEventArgs>((e) => { OnProjectItemsChanged(e); }));

            thisLock = new Object();
        }

        /// <summary>
        /// Show the DockPane.
        /// </summary>
        internal static void Show()
        {
            DockPane _dockPane = FrameworkApplication.DockPaneManager.Find(_dockPaneID);
            if (_dockPane == null)
                return;

            _dockPane.Activate();
        }

        #region Properties and Backing Fields

        /// <summary>
        /// Text shown near the top of the DockPane.
        /// </summary>
        private string _heading = "My DockPane";
        public string Heading
        {
            get { return _heading; }
            set { SetProperty(ref _heading, value, () => Heading); }
        }

        private string _cityZoomCompleted;
        public string CityZoomCompleted
        {
            get { return _cityZoomCompleted; }
            set { SetProperty(ref _cityZoomCompleted, value, () => CityZoomCompleted); }
        }

        private List<string> _cityNames;
        public List<string> CityNames
        {
            get { return _cityNames; }
            private set { SetProperty(ref _cityNames, value, () => CityNames); }
        }

        private string _neighborhoodZoomCompleted;
        public string NeighborhoodZoomCompleted
        {
            get { return _neighborhoodZoomCompleted; }
            set { SetProperty(ref _neighborhoodZoomCompleted, value, () => NeighborhoodZoomCompleted); }
        }

        private List<string> _neighborhoodNames;
        public List<string> NeighborhoodNames
        {
            get { return _neighborhoodNames; }
            private set { SetProperty(ref _neighborhoodNames, value, () => NeighborhoodNames); }
        }

        #endregion

        #region Event Handlers

        private void OnProjectOpened(ProjectEventArgs e)
        {
            UpdateCityNames(e.Project);
        }

        private void OnProjectItemsChanged(ProjectItemsChangedEventArgs e)
        {
            UpdateCityNames(Project.Current);
        }

        private void OnUnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
        {
            Console.WriteLine("---UNOBSERVED TASK EXCEPTION---");
            Console.WriteLine(e.Exception.Message);
            foreach (Exception ex in e.Exception.InnerExceptions)
            {
                Console.WriteLine(ex.ToString());
            }
            e.SetObserved();
        }

        #endregion

        #region Task Wrappers

        private static Task<MapFrame> WrapTask(Task<MapFrame> task)
        {
            var stackTrace = new System.Diagnostics.StackTrace(true);
            var taskCompletionSource = new TaskCompletionSource<MapFrame>();

            task.ContinueWith(async x =>
            {
                if (x.IsFaulted)
                {
                    taskCompletionSource.TrySetException(new FieldAccessException("Stack Trace: " + stackTrace, x.Exception.GetBaseException()));
                }
                else if (x.IsCanceled)
                {
                    taskCompletionSource.TrySetCanceled();
                }
                else
                {
                    taskCompletionSource.TrySetResult(await task);
                }
            });

            return taskCompletionSource.Task;
        }

        private static Task<bool> WrapTask(Task<bool> task)
        {
            var stackTrace = new System.Diagnostics.StackTrace(true);
            var taskCompletionSource = new TaskCompletionSource<bool>();

            task.ContinueWith(async x =>
            {
                if (x.IsFaulted)
                {
                    taskCompletionSource.TrySetException(new BadImageFormatException("Stack Trace: " + stackTrace, x.Exception.GetBaseException()));
                }
                else if (x.IsCanceled)
                {
                    taskCompletionSource.TrySetCanceled();
                }
                else
                {
                    taskCompletionSource.TrySetResult(await task);
                }
            });

            return taskCompletionSource.Task;
        }

        private static Task<RowCursor> WrapTask(Task<RowCursor> task)
        {
            var stackTrace = new System.Diagnostics.StackTrace(true);
            var taskCompletionSource = new TaskCompletionSource<RowCursor>();

            task.ContinueWith(async x =>
            {
                if (x.IsFaulted)
                {
                    taskCompletionSource.TrySetException(new GeodatabaseCursorException("Stack Trace: " + stackTrace, x.Exception.GetBaseException()));
                }
                else if (x.IsCanceled)
                {
                    taskCompletionSource.TrySetCanceled();
                }
                else
                {
                    taskCompletionSource.TrySetResult(await task);
                }
            });

            return taskCompletionSource.Task;
        }

        private static Task<Envelope> WrapTask(Task<Envelope> task)
        {
            var stackTrace = new System.Diagnostics.StackTrace(true);
            var taskCompletionSource = new TaskCompletionSource<Envelope>();

            task.ContinueWith(async x =>
            {
                if (x.IsFaulted)
                {
                    taskCompletionSource.TrySetException(new DuplicateWaitObjectException("Stack Trace: " + stackTrace, x.Exception.GetBaseException()));
                }
                else if (x.IsCanceled)
                {
                    taskCompletionSource.TrySetCanceled();
                }
                else
                {
                    taskCompletionSource.TrySetResult(await task);
                }
            });

            return taskCompletionSource.Task;
        }

        private static Task<Dictionary<BasicFeatureLayer, List<long>>> WrapTask(Task<Dictionary<BasicFeatureLayer, List<long>>> task)
        {
            var stackTrace = new System.Diagnostics.StackTrace(true);
            var taskCompletionSource = new TaskCompletionSource<Dictionary<BasicFeatureLayer, List<long>>>();

            task.ContinueWith(async x =>
            {
                if (x.IsFaulted)
                {
                    taskCompletionSource.TrySetException(new MulticastNotSupportedException("Stack Trace: " + stackTrace, x.Exception.GetBaseException()));
                }
                else if (x.IsCanceled)
                {
                    taskCompletionSource.TrySetCanceled();
                }
                else
                {
                    taskCompletionSource.TrySetResult(await task);
                }
            });

            return taskCompletionSource.Task;
        }

        private static Task<Geometry> WrapTask(Task<Geometry> task)
        {
            var stackTrace = new System.Diagnostics.StackTrace(true);
            var taskCompletionSource = new TaskCompletionSource<Geometry>();

            task.ContinueWith(async x =>
            {
                if (x.IsFaulted)
                {
                    taskCompletionSource.TrySetException(new EntryPointNotFoundException("Stack Trace: " + stackTrace, x.Exception.GetBaseException()));
                }
                else if (x.IsCanceled)
                {
                    taskCompletionSource.TrySetCanceled();
                }
                else
                {
                    taskCompletionSource.TrySetResult(await task);
                }
            });

            return taskCompletionSource.Task;
        }

        private static Task<double> WrapTask(Task<double> task)
        {
            var stackTrace = new System.Diagnostics.StackTrace(true);
            var taskCompletionSource = new TaskCompletionSource<double>();

            task.ContinueWith(async x =>
            {
                if (x.IsFaulted)
                {
                    taskCompletionSource.TrySetException(new KeyNotFoundException("Stack Trace: " + stackTrace, x.Exception.GetBaseException()));
                }
                else if (x.IsCanceled)
                {
                    taskCompletionSource.TrySetCanceled();
                }
                else
                {
                    taskCompletionSource.TrySetResult(await task);
                }
            });

            return taskCompletionSource.Task;
        }

        private static Task<Task<bool>> WrapTask(Task<Task<bool>> task)
        {
            var stackTrace = new System.Diagnostics.StackTrace(true);
            var taskCompletionSource = new TaskCompletionSource<Task<bool>>();

            task.ContinueWith(async x =>
            {
                if (x.IsFaulted)
                {
                    taskCompletionSource.TrySetException(new EncoderFallbackException("Stack Trace: " + stackTrace, x.Exception.GetBaseException()));
                }
                else if (x.IsCanceled)
                {
                    taskCompletionSource.TrySetCanceled();
                }
                else
                {
                    taskCompletionSource.TrySetResult(await task);
                }
            });

            return taskCompletionSource.Task;
        }

        Task<EnvelopeBuilder> WrapTask(Task<EnvelopeBuilder> task)
        {
            var stackTrace = new System.Diagnostics.StackTrace(true);
            var taskCompletionSource = new TaskCompletionSource<EnvelopeBuilder>();

            task.ContinueWith(async x =>
            {
                if (x.IsFaulted)
                {
                    taskCompletionSource.TrySetException(new TypeLoadException("Stack Trace: " + stackTrace, x.Exception.GetBaseException()));
                }
                else if (x.IsCanceled)
                {
                    taskCompletionSource.TrySetCanceled();
                }
                else
                {
                    taskCompletionSource.TrySetResult(await task);
                }
            });

            return taskCompletionSource.Task;
        }

        Task WrapTask(Task task)
        {
            var stackTrace = new System.Diagnostics.StackTrace(true);
            var taskCompletionSource = new TaskCompletionSource<Object>();

            task.ContinueWith(x =>
            {
                if (x.IsFaulted)
                {
                    taskCompletionSource.TrySetException(new TypeLoadException("Stack Trace: " + stackTrace, x.Exception.GetBaseException()));
                }
                else if (x.IsCanceled)
                {
                    taskCompletionSource.TrySetCanceled();
                }
            });

            return taskCompletionSource.Task;
        }

        #endregion

        #region Update Properties

        /// <summary>
        ///     Updates the list of city names using the contents of the referenced project
        /// </summary>
        /// <param name="project">
        ///     A reference to the project from which city names will be drawn
        /// </param>
        public void UpdateCityNames(Project project)
        {
            IEnumerable<MapProjectItem> mapProjectItems = project.GetItems<MapProjectItem>();
            var cityNames = new List<string>();
            foreach(MapProjectItem mpi in mapProjectItems)
            {
                string[] values = mpi.Name.Split(' ');
                if (!cityNames.Contains(values[0]))
                {
                    cityNames.Add(values[0]);
                }
            }
            CityNames = cityNames;
        }

        /// <summary>
        ///     Updates the list of neighborhood names to include only the neighborhoods in the newly selected city.
        ///     TODO: Check map layers to determine the format of the data and handle accordingly
        /// </summary>
        /// <param name="cityName">
        ///     The name of the selected city.
        /// </param>
        /// <returns></returns>
        public async Task UpdateNeighborhoodNamesAsync(string cityName)
        {
            string mpiName = cityName + " Neighborhoods";
            List<string> neighborhoodNamesList;

            Map map = await GetMapAsync(mpiName);
            if(GetLayer(_layerName, map) != null)
            {
                RowCursor rowCursor = await GetRowCursorAsync(map).ConfigureAwait(false);
                int neighoodIndex = await QueuedTask.Run(() => rowCursor.FindField("Neighood")).ConfigureAwait(false);
                neighborhoodNamesList = await GetRowValuesAsync(rowCursor, neighoodIndex).ConfigureAwait(false);
            }
            else
            {
                neighborhoodNamesList = await GetNeighborhoodLayerNamesAsync(map);
            }
            
            neighborhoodNamesList.Remove("");
            NeighborhoodNames = neighborhoodNamesList;
        }

        /// <summary>
        ///     Updates the layout's map frames to show the maps associated with the newly selected city.
        ///     TODO: Update text elements of the layout
        /// </summary>
        /// <param name="cityName">
        ///     The name of the selected city
        /// </param>
        /// <returns></returns>
        public async Task UpdateLayoutMapFrames(string cityName)
        {
            string neighborhoodMpiName = cityName + " Neighborhoods";
            string insetMpiName = cityName + " Inset";

            var stuff = new Dictionary<string, Task<MapFrame>>();
            stuff.Add(insetMpiName, GetInsetMapFrameAsync());
            stuff.Add(neighborhoodMpiName, GetNeighborhoodMapFrameAsync());

            var setMapTasks = new List<Task>();
            foreach(KeyValuePair<string, Task<MapFrame>> pair in stuff)
            {
                setMapTasks.Add(QueuedTask.Run(
                    async () => (await pair.Value).SetMap(await GetMapAsync(pair.Key))));
            }
            await Task.WhenAll(setMapTasks).ConfigureAwait(false);
        }

        /// <summary>
        ///     Performs the work that necessarily follows a change in city selection
        /// </summary>
        /// <param name="cityName">
        ///     The name of the selected city
        /// </param>
        /// <returns>
        ///     A Task representing the work to be performed asynchronously
        /// </returns>
        public async Task ChangeCitySelection(string cityName)
        {
            await UpdateNeighborhoodNamesAsync(cityName).ConfigureAwait(false);
            await UpdateLayoutMapFrames(cityName).ConfigureAwait(false);

            CityZoomCompleted = "Focusing...";
            QueryFilter cityQueryFilter = GetCityQueryFilter();
            var zoomToExtentTasks = new List<Task<bool>>();
            zoomToExtentTasks.Add(ChangeExtent(await GetInsetMapFrameAsync(), cityQueryFilter));
            zoomToExtentTasks.Add(ChangeExtent(await GetNeighborhoodMapFrameAsync(), cityQueryFilter));
            bool[] statuses = await Task.WhenAll(zoomToExtentTasks);
            string completed = "";
            foreach(bool status in statuses)
            {
                completed += status.ToString() + " ,";
            }
        }

        public async Task ChangeNeighborhoodSelection(SelectionChangedEventArgs eventArgs)
        {
            var deselectedNeighborhoods = new List<string>();
            var selectedNeighborhoods = new List<string>();

            foreach(string item in eventArgs.RemovedItems)
            {
                deselectedNeighborhoods.Add(item);
            }

            foreach(string item in eventArgs.AddedItems)
            {
                selectedNeighborhoods.Add(item);
            }

            MapFrame neighborhoodMapFrame = await GetNeighborhoodMapFrameAsync();
            MapFrame insetMapFrame = await GetInsetMapFrameAsync();

            FeatureLayer neighborhoodFeatureLayer = GetFeatureLayer(neighborhoodMapFrame);
            FeatureLayer insetFeatureLayer = GetFeatureLayer(insetMapFrame);

            List<FeatureLayer> featureLayers = new List<FeatureLayer>();
            featureLayers.Add(neighborhoodFeatureLayer);
            featureLayers.Add(insetFeatureLayer);

            List<Task> updateSymbologyTasks = new List<Task>();
            foreach (FeatureLayer featureLayer in featureLayers)
            {
                if(deselectedNeighborhoods != null)
                {
                    foreach (string deselectedNeighborhood in deselectedNeighborhoods)
                    {
                        updateSymbologyTasks.Add(UpdateNeighborhoodSymbology(featureLayer, deselectedNeighborhood, false));
                    }
                }
                if(selectedNeighborhoods != null)
                {
                    foreach (string selectedNeighborhood in selectedNeighborhoods)
                    {
                        updateSymbologyTasks.Add(UpdateNeighborhoodSymbology(featureLayer, selectedNeighborhood, true));
                    }
                }
            }
            await Task.WhenAll(updateSymbologyTasks);

            NeighborhoodZoomCompleted = "Focusing...";
            if(selectedNeighborhoods != null)
            {
                NeighborhoodZoomCompleted = 
                    (await ChangeExtent(neighborhoodMapFrame, GetNeighborhoodQueryFilter(selectedNeighborhoods.FirstOrDefault()))).ToString();
            }
        }

        #endregion

        #region Get Model Data

        private LayoutProjectItem GetLayoutProjectItem()
        {
            return Project.Current.GetItems<LayoutProjectItem>().FirstOrDefault(
                (item) => item.Name.Equals(_layoutName));
        }

        private async Task<Layout> GetLayoutAsync()
        {
            return await QueuedTask.Run(() => GetLayoutProjectItem().GetLayout());
        }

        private async Task<MapFrame> GetInsetMapFrameAsync()
        {
            return (await GetLayoutAsync()).FindElement("Inset Map Frame") as MapFrame;
        }

        private async Task<MapFrame> GetNeighborhoodMapFrameAsync()
        {
            return (await GetLayoutAsync()).FindElement("Neighborhood Map Frame") as MapFrame;
        }

        private FeatureLayer GetFeatureLayer(MapFrame mapFrame)
        {
            return GetLayer(_layerName, mapFrame.Map) as FeatureLayer;
        }

        private Layer GetLayer(string layerName, Map map)
        {
            return map.Layers.FirstOrDefault((x) => x.Name.Equals(layerName));
        }

        private async Task<FeatureClass> GetFeatureClassAsync(Map map, string layerName=_layerName)
        {
            Layer layer = GetLayer(layerName, map);
            var fLayer = layer as FeatureLayer;
            return await QueuedTask.Run(() => fLayer.GetFeatureClass()).ConfigureAwait(false);
        }

        private async Task<Map> GetMapAsync(string mpiName)
        {
            MapProjectItem mpi = Project.Current.GetItems<MapProjectItem>().FirstOrDefault((i) => i.Name.Equals(mpiName));
            return await QueuedTask.Run(() => mpi.GetMap()).ConfigureAwait(false);
        }

        private async Task<Layout> GetLayoutAsync(string lpiName)
        {
            LayoutProjectItem lpi = Project.Current.GetItems<LayoutProjectItem>().FirstOrDefault((i) => i.Name.Equals(lpiName));
            return await QueuedTask.Run(() => lpi.GetLayout()).ConfigureAwait(false);
        }

        private async Task<RowCursor> GetRowCursorAsync(Map map, QueryFilter queryFilter = null, string layerName = _layerName)
        {
            // Map map = await GetMapAsync(mpiName).ConfigureAwait(false);
            FeatureClass featureClass = await GetFeatureClassAsync(map, layerName).ConfigureAwait(false);
            return await QueuedTask.Run(() => featureClass.Search(queryFilter)).ConfigureAwait(false);
        }

        private async Task<List<string>> GetRowValuesAsync(RowCursor rowCursor, int textVariableIndex)
        {
            List<Task<string>> getValueTasks = new List<Task<string>>();
            do
            {
                Row row = rowCursor.Current;
                if (row != null)
                {
                    getValueTasks.Add(QueuedTask.Run(() => row.GetOriginalValue(textVariableIndex).ToString()));
                }

            } while (await QueuedTask.Run(() => rowCursor.MoveNext()).ConfigureAwait(false));
            string[] names = await Task.WhenAll(getValueTasks).ConfigureAwait(false);
            List<string> namesList = names.Distinct().ToList();
            namesList.Remove("");
            return namesList;
        }

        private async Task<List<string>> GetNeighborhoodLayerNamesAsync(Map map)
        {
            var names = new List<string>();
            foreach(Layer layer in map.Layers)
            {
                RowCursor rowCursor = await GetRowCursorAsync(map, null, layer.Name);
                if(!names.Contains(layer.Name) && rowCursor.FindField("TRACTCE") >= 0)
                {
                    names.Add(layer.Name);
                }
            }
            names.Remove("");
            return names;
        }

        private async Task<Envelope> GetEnvelopeAsync(RowCursor rowCursor)
        {
            ConcurrentDictionary<string, double> extentBounds = new ConcurrentDictionary<string, double>();
            extentBounds.TryAdd("xMin", 0.0);
            extentBounds.TryAdd("xMax", 0.0);
            extentBounds.TryAdd("yMin", 0.0);
            extentBounds.TryAdd("yMax", 0.0);

            List<Task> calculateExtentTasks = new List<Task>();
            do
            {
                calculateExtentTasks.Add(Task.Run(async () =>
                {
                    double xMin, xMax, yMin, yMax;
                    bool xMinFound = extentBounds.TryGetValue("xMin", out xMin);
                    bool xMaxFound = extentBounds.TryGetValue("xMax", out xMax);
                    bool yMinFound = extentBounds.TryGetValue("yMin", out yMin);
                    bool yMaxFound = extentBounds.TryGetValue("yMax", out yMax);

                    Feature feature = rowCursor.Current as Feature;
                    if (feature != null)
                    {
                        Task<Geometry> getShapeTask = QueuedTask.Run(() => feature.GetShape());
                        Envelope extent = (await getShapeTask.ConfigureAwait(false)).Extent;

                        if (xMin == 0.0 || extent.XMin < xMin)
                        {
                            bool xMinUpdated = extentBounds.TryUpdate("xMin", extent.XMin, xMin);
                        }
                        if (xMax == 0.0 || extent.XMax > xMax)
                        {
                            bool xMaxUpdated = extentBounds.TryUpdate("xMax", extent.XMax, xMax);
                        }
                        if (yMin == 0.0 || extent.YMin < yMin)
                        {
                            bool yMinUpdated = extentBounds.TryUpdate("yMin", extent.YMin, yMin);
                        }
                        if (yMax == 0.0 || extent.YMax > yMax)
                        {
                            bool yMaxUpdate = extentBounds.TryUpdate("yMax", extent.YMax, yMax);
                        }
                    }
                }));
            } while (await QueuedTask.Run(() => rowCursor.MoveNext()));

            EnvelopeBuilder eb = await QueuedTask.Run(() => new EnvelopeBuilder());
            var setEnvelopePropertyTasks = new List<Task>();
            double xMinFinal, xMaxFinal, yMinFinal, yMaxFinal;
            await Task.WhenAll(calculateExtentTasks);

            bool xMinFinalFound = extentBounds.TryGetValue("xMin", out xMinFinal);
            bool xMaxFinalFound = extentBounds.TryGetValue("xMax", out xMaxFinal);
            bool yMinFinalFound = extentBounds.TryGetValue("yMin", out yMinFinal);
            bool yMaxFinalFound = extentBounds.TryGetValue("yMax", out yMaxFinal);

            setEnvelopePropertyTasks.Add(QueuedTask.Run(() => eb.XMin = xMinFinal));
            setEnvelopePropertyTasks.Add(QueuedTask.Run(() => eb.XMax = xMaxFinal));
            setEnvelopePropertyTasks.Add(QueuedTask.Run(() => eb.YMin = yMinFinal));
            setEnvelopePropertyTasks.Add(QueuedTask.Run(() => eb.YMax = yMaxFinal));

            await Task.WhenAll(setEnvelopePropertyTasks.ToArray());
            return await QueuedTask.Run(() => eb.ToGeometry());
        }

        private async Task<MapFrame> GetMapFrameAsync(string layoutElementName)
        {
            Task<Layout> layoutTask = GetLayoutAsync(_layoutName);
            var mapFrame = (await layoutTask.ConfigureAwait(false)).FindElement(layoutElementName) as MapFrame;
            return mapFrame;
        }

        private QueryFilter GetNeighborhoodQueryFilter(string neighborhoodName)
        {
            QueryFilter queryFilter = new QueryFilter()
            {
                WhereClause = "Neighood ='" + neighborhoodName + "'"
            };
            return queryFilter;
        }

        private QueryFilter GetCityQueryFilter()
        {
            QueryFilter queryFilter = new QueryFilter()
            {
                WhereClause = "NOT(Neighood IS NULL)"
            };
            return queryFilter;
        }

        #endregion

        #region Zoom to Features

        /// <summary>
        /// Zooms the MapFrame to the extent of the specified features
        /// </summary>
        /// <param name="mapFrame">The MapFrame whose extent will be changed</param>
        /// <param name="queryFilter">The features whose extent used</param>
        private async Task<bool> ChangeExtent(MapFrame mapFrame, QueryFilter queryFilter=null)
        {
            Task<RowCursor> rowCursorTask = GetRowCursorAsync(mapFrame.Map, queryFilter);
            Task<Envelope> extentTask = GetEnvelopeAsync(await rowCursorTask.ConfigureAwait(false));
            return await await QueuedTask.Run(async () => mapFrame.MapView.ZoomToAsync(await extentTask));
        }

        #endregion

        #region Update Symbology

        /// <summary>
        ///     Highlights a selected neighborhood or greys a deselected one
        /// </summary>
        /// <param name="featureLayer"></param>
        ///     The layer containing the feature whose symbology is to be updated
        /// <param name="neighborhood"></param>
        ///     Must be equal to one of the labels of featureLayer's renderer's uniqueValueGroup's classes
        /// <param name="selection"></param>
        ///     If true, highlights the neighborhood. Otherwise, greys it
        /// <returns></returns>
        private async Task UpdateNeighborhoodSymbology(FeatureLayer featureLayer, string neighborhood, bool selection)
        {
            var renderer = await QueuedTask.Run(() => featureLayer.GetRenderer());

            var uniqueValueRenderer = renderer as CIMUniqueValueRenderer;
            CIMUniqueValueGroup uniqueValueGroup = uniqueValueRenderer.Groups.FirstOrDefault();
            CIMUniqueValueClass neighborhoodClass = null;
            int classIndex = -1;
            for(int i = 0; i < uniqueValueGroup.Classes.Count(); i++)
            {
                if (uniqueValueGroup.Classes[i].Label.Equals(neighborhood))
                {
                    neighborhoodClass = uniqueValueGroup.Classes[i];
                    classIndex = i;
                }
            }
            var polygonSymbol = neighborhoodClass.Symbol.Symbol as CIMPolygonSymbol;

            /* var solidFillSymbolLayer = polygonSymbol.SymbolLayers.FirstOrDefault(
                (x) => x.GetType().Equals(typeof(CIMSolidFill))) as CIMSolidFill; */

            CIMColor color;

            if (selection)
            {
                var fixedColorRamp = uniqueValueRenderer.ColorRamp as CIMFixedColorRamp;
                color = fixedColorRamp.Colors[classIndex - 1];
            }
            else // deselection
            {
                color = CIMColor.CreateGrayColor(50);
            }

            SymbolFactory.SetColor(polygonSymbol, color);

            /* solidFillSymbolLayer.ColorLocked = false;
            solidFillSymbolLayer.Color = color; */

            await QueuedTask.Run(() => featureLayer.SetRenderer(renderer));
        }

        #endregion 
    }

    /// <summary>
    /// Button implementation to show the DockPane.
    /// </summary>
    internal class Dockpane1_ShowButton : ArcGIS.Desktop.Framework.Contracts.Button
    {
        protected override void OnClick()
        {
            Dockpane1ViewModel.Show();
        }
    }
}
