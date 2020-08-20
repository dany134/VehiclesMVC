using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DataAcessLayer.Context;
using VehicleMVCProject.ViewModels;
using DataAcessLayer.Models;
using AutoMapper;
using PagedList;
using VehicleMVCProject.Auto_Mapper;
using DataAcessLayer.Interfaces;
using DataAcessLayer.Service;
using DataAcessLayer.Extensions;

namespace VehicleMVCProject.Controllers
{
    public class VehicleModelController : Controller
    {
        private IVehicleModelService _modelService;
        private IVehicleMakeService _makeService;
        private IMapper _mapper;
     
        public VehicleModelController(IMapper mapper, IVehicleModelService vehicleService, IVehicleMakeService makeService)
        {
            _makeService = makeService;
            this._mapper = mapper;
            _modelService = vehicleService;
        }

        // GET: VehicleModel
        public async Task<ActionResult> Index(string vehiclemake, string search, int? page, string currentFilter, string sortOrder)
        {
            var modelmake = await _modelService.GetModels();
            ViewBag.Search = search;
            string src = "";
            if(vehiclemake != null)
            {
                src = vehiclemake;
            }
            else
            {
                src = search;
            }
            VehicleFilters filter = new VehicleFilters(src, currentFilter);
            VehiclePaging paging = new VehiclePaging(page);
          
            var models = await _modelService.GetModelsList(filter, paging);
            var makes = modelmake.Select(m => m.VehicleMake.Name).Distinct();
            
            ViewBag.VehicleMake = new SelectList(makes);
            List<VehicleModelViewModel> viewModel = _mapper.Map<List<VehicleModelViewModel>>(models);
            var pagedList = new StaticPagedList<VehicleModelViewModel>(viewModel, paging.Page ?? 1, paging.PageSize, paging.TotalItems);
            FilteringCheck(ViewBag, filter, paging);
            return View(pagedList);
        }

        // GET: VehicleModel/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VehicleModel vehicleModel = await _modelService.GetModelById(id);
            VehicleModelViewModel viewModel = _mapper.Map<VehicleModelViewModel>(vehicleModel);
            if (vehicleModel == null)
            {
                return HttpNotFound();
            }
            return View(viewModel);
        }

        // GET: VehicleModel/Create
        public async Task<ActionResult> Create()
        {
            IEnumerable<VehicleMake> makes = await _makeService.GetMakes();

            ViewBag.VehicleMakeID = new SelectList(makes, "Id", "Name");
            return View();
        }

        // POST: VehicleModel/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name,Abrv,VehicleMakeID")] VehicleModel vehicleModel)
        {
            if (ModelState.IsValid)
            {
                await _modelService.InsertModel(vehicleModel);
                return RedirectToAction("Index");
            }
            IEnumerable<VehicleMake> makes = await _makeService.GetMakes();
            ViewBag.VehicleMakeID = new SelectList(makes, "Id", "Name", vehicleModel.VehicleMakeID);
            VehicleModelViewModel viewModel = _mapper.Map<VehicleModelViewModel>(vehicleModel);
            return View(viewModel);
        }

        // GET: VehicleModel/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VehicleModel vehicleModel = await _modelService.GetModelById(id);
            
            if (vehicleModel == null)
            {
                return HttpNotFound();
            }
            IEnumerable<VehicleMake> makes = await _makeService.GetMakes();
            ViewBag.VehicleMakeID = new SelectList(makes, "Id", "Name", vehicleModel.VehicleMakeID);
            VehicleModelViewModel viewModel = _mapper.Map<VehicleModelViewModel>(vehicleModel);
            return View(viewModel);
        }

        // POST: VehicleModel/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,Abrv,VehicleMakeID")] VehicleModel vehicleModel)
        {
            if (ModelState.IsValid)
            {
               await _modelService.UpdateModel(vehicleModel);
                return RedirectToAction("Index");
            }
            IEnumerable<VehicleMake> makes = await _makeService.GetMakes();
            ViewBag.VehicleMakeID = new SelectList(makes, "Id", "Name", vehicleModel.VehicleMakeID);
            VehicleModelViewModel viewModel = _mapper.Map<VehicleModelViewModel>(vehicleModel);
            return View(viewModel);
        }

        // GET: VehicleModel/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VehicleModel vehicleModel = await _modelService.GetModelById(id);
            if (vehicleModel == null)
            {
                return HttpNotFound();
            }
            VehicleModelViewModel viewModel = _mapper.Map<VehicleModelViewModel>(vehicleModel);
            return View(viewModel);
        }

        // POST: VehicleModel/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            VehicleModel vehicleModel = await _modelService.GetModelById(id);
            await _modelService.DeleteModel(vehicleModel);
            return RedirectToAction("Index");
        }
        private void FilteringCheck(dynamic ViewBag, VehicleFilters filter, VehiclePaging page)
        {
            if(filter.SearchString != null)
            {
                page.Page = 1;
            }
            else
            {
                filter.SearchString = filter.CurrentFilter;
            }
            ViewBag.CurrentFilter = filter.SearchString;
        }

      
    }
}
