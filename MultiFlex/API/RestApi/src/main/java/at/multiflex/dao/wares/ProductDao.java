package at.multiflex.dao.wares;

import at.multiflex.dao.logic.ProductLogic;
import at.multiflex.dto.traffic.CategoryProducts;
import at.multiflex.dto.wares.ProductDto;
import at.multiflex.mapper.wares.ProductMapper;
import at.multiflex.model.Category;
import at.multiflex.repository.CategoryRepository;
import at.multiflex.repository.wares.ProductRepository;

import javax.enterprise.context.Dependent;
import javax.inject.Inject;
import javax.ws.rs.*;
import javax.ws.rs.core.MediaType;
import javax.ws.rs.core.Response;
import java.util.List;

@Dependent
@Path("/Product")
public class ProductDao {
    @Inject
    ProductRepository repository;

    @Inject
    ProductMapper mapper;

    @Inject
    ProductLogic productLogic;

    //<editor-fold desc="Get">
    @GET
    @Produces(MediaType.APPLICATION_JSON_PATCH_JSON)
    public List<ProductDto> getAll() {
        var entities = repository.loadAll();
        return mapper.toDto(entities);
    }

    @GET
    @Produces(MediaType.APPLICATION_JSON_PATCH_JSON)
    @Path("/{name}")
    public List<ProductDto> getByName(String name) {
        var entities = repository.findByName(name);
        return mapper.toDto(entities);
    }
    //<editor-fold desc="Get">
    @GET
    @Produces(MediaType.APPLICATION_JSON_PATCH_JSON)
    @Path("/category/{name}")
    public CategoryProducts getByCategory(String name) {
        return productLogic.getProductsByByCategory(name);
    }
    @GET
    @Produces(MediaType.APPLICATION_JSON_PATCH_JSON)
    @Path("/category")
    public List<CategoryProducts> getAllByCategory(String name) {
        return productLogic.getAllProductsByByCategory();
    }
    //</editor-fold>
    @GET
    @Produces(MediaType.APPLICATION_JSON_PATCH_JSON)
    @Path("/{id}")
    public ProductDto getById(Integer id) {
        var entity = repository.findById(id);
        return mapper.toDto(entity);
    }
    //</editor-fold>
    //<editor-fold desc="Post">
    @POST
    @Produces(MediaType.APPLICATION_JSON_PATCH_JSON)
    @Path("/add")
    public Response add(ProductDto dto) {
        var entity = mapper.fromDto(dto);
        repository.add(entity);
        return Response.status(Response.Status.CREATED).build();
    }
    //</editor-fold>
    //<editor-fold desc="Delete">
    @DELETE
    @Produces(MediaType.APPLICATION_JSON_PATCH_JSON)
    @Path("/delete/{id}")
    public Response delete(@PathParam("id") Integer id) {
        var entity = repository.findById(id);
        if(entity == null) {
            throw new NotFoundException();
        }
        repository.delete(entity);
        return Response.status(Response.Status.NO_CONTENT).build();
    }
    //</editor-fold>
    //<editor-fold desc="Put">
    @PUT
    @Produces(MediaType.APPLICATION_JSON_PATCH_JSON)
    @Path("/update")
    public Response update(ProductDto dto) {
        var entity = mapper.fromDto(dto);
        repository.update(entity);
        return Response.status(Response.Status.OK).build();
    }
    //</editor-fold>
}
