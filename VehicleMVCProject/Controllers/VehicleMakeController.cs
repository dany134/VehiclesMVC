﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DataAcessLayer.Context;
using PagedList;
using DataAcessLayer.Models;
using AutoMapper;
using VehicleMVCProject.ViewModels;
using VehicleMVCProject.Auto_Mapper;
using DataAcessLayer.Interfaces;
using DataAcessLayer.Service;
using Ninject;
using DataAcessLayer.Extensions;

namespace VehicleMVCProject.Controllers
{
    public class VehicleMakeController : Controller
    {
        private VehicleContext db = new VehicleContext();
        private IVehicleMakeService _vehicleService;
        private IMapper _mapper;

        public VehicleMakeController(IMapper mapper, IVehicleMakeService vehicleService)
        {
            this._mapper = mapper;
            this._vehicleService = vehicleService;

        }

        // GET: VehicleMake
        public async Task<ActionResult> Index(string searchString, int? page, string currentFilter, string sortOrder)
        {

            VehicleFilters filter = new VehicleFilters(searchString, currentFilter);
            VehiclePaging paging = new VehiclePaging(page);
            var makes = await _vehicleService.GetMakesList(filter, paging);

            List<VehicleMakeViewModel> viewModel = _mapper.Map<List<VehicleMakeViewModel>>(makes);
            var pagedList = new StaticPagedList<VehicleMakeViewModel>(viewModel, paging.Page ?? 1, paging.PageSize, paging.TotalItems);
            SortingCheck(ViewBag, filter, paging);
            return View(pagedList);

        }

        // GET: VehicleMake/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VehicleMake vehicleMake = await _vehicleService.GetMakeById(id);
            VehicleMakeViewModel viewModel = _mapper.Map<VehicleMakeViewModel>(vehicleMake);
            if (vehicleMake == null)
            {
                return HttpNotFound();
            }
            return View(viewModel);
        }

        // GET: VehicleMake/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: VehicleMake/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name,Abrv")] VehicleMake vehicleMake)
        {

            if (ModelState.IsValid)
            {
                await _vehicleService.InsertMake(vehicleMake);
                return RedirectToAction("Index");
            }
            VehicleMakeViewModel viewModel = _mapper.Map<VehicleMakeViewModel>(vehicleMake);
            return View(viewModel);
        }

        // GET: VehicleMake/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VehicleMake vehicleMake = await _vehicleService.GetMakeById(id);
            if (vehicleMake == null)
            {
                return HttpNotFound();
            }
            VehicleMakeViewModel viewModel = _mapper.Map<VehicleMakeViewModel>(vehicleMake);
            return View(viewModel);
        }

        // POST: VehicleMake/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,Abrv")] VehicleMake vehicleMake)
        {
            if (ModelState.IsValid)
            {
                await _vehicleService.UpdateMake(vehicleMake);
                return RedirectToAction("Index");
            }
            VehicleMakeViewModel viewModel = _mapper.Map<VehicleMakeViewModel>(vehicleMake);
            return View(viewModel);
        }

        // GET: VehicleMake/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VehicleMake vehicleMake = await _vehicleService.GetMakeById(id);
            if (vehicleMake == null)
            {
                return HttpNotFound();
            }
            VehicleMakeViewModel viewModel = _mapper.Map<VehicleMakeViewModel>(vehicleMake);

            return View(viewModel);
        }

        // POST: VehicleMake/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            VehicleMake vehicleMake = await _vehicleService.GetMakeById(id);
            await _vehicleService.DeleteMake(vehicleMake);
            return RedirectToAction("Index");
        }
        private void SortingCheck(dynamic ViewBag, VehicleFilters filter, VehiclePaging page)
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
