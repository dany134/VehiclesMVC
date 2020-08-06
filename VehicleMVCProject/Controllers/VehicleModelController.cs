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
namespace VehicleMVCProject.Controllers
{
    public class VehicleModelController : Controller
    {
        private VehicleContext db = new VehicleContext();
        private IVehicleModelService _modelService;    
        private IMapper _mapper;
     
        public VehicleModelController(IMapper mapper, IVehicleModelService vehicleService)
        {
            this._mapper = mapper;
            _modelService = vehicleService;
        }

        // GET: VehicleModel
        public async Task<ActionResult> Index(string vehiclemake, string search, int? page, string currentFilter, string sortOrder)
        {
            ViewBag.CurrentSort = sortOrder;
            IEnumerable<VehicleModel> vehicleModels = await _modelService.GetModels();
           if(search != null)
            {
                page = 1;
            }
            else
            {
                search = currentFilter;
            }
            ViewBag.CurrentFilter = search;

            if (!String.IsNullOrEmpty(search))
            {
                vehicleModels = vehicleModels.Where(v => v.Name.Contains(search) || v.Abrv.Contains(search) || v.VehicleMake.Name.Contains(search));
                ViewBag.Search = search;
            }
            var makes = vehicleModels.OrderBy(v => v.VehicleMake.Name).Select(v => v.VehicleMake.Name).Distinct();
            if (!String.IsNullOrEmpty(vehiclemake))
            {
                vehicleModels = vehicleModels.Where(v => v.VehicleMake.Name == vehiclemake);
            }

            int pageSize = 3;
            int pageNumber = (page ?? 1);
            ViewBag.VehicleMake = new SelectList(makes);
            List<VehicleModelViewModel> viewModel = _mapper.Map<List<VehicleModelViewModel>>(vehicleModels);
            return View(viewModel.ToPagedList(pageNumber, pageSize));
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
        public ActionResult Create()
        {
            ViewBag.VehicleMakeID = new SelectList(db.VehicleMakes, "Id", "Name");
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

            ViewBag.VehicleMakeID = new SelectList(db.VehicleMakes, "Id", "Name", vehicleModel.VehicleMakeID);
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
            ViewBag.VehicleMakeID = new SelectList(db.VehicleMakes, "Id", "Name", vehicleModel.VehicleMakeID);
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
            ViewBag.VehicleMakeID = new SelectList(db.VehicleMakes, "Id", "Name", vehicleModel.VehicleMakeID);
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

      
    }
}
