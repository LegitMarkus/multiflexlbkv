package org.acme.mapper;

import org.acme.DTO.RegalDto;
import org.acme.DTO.ShelfDto;
import org.acme.DTO.WareDto;
import org.acme.model.Regal;
import org.acme.model.Shelf;
import org.acme.model.Ware;

import javax.enterprise.context.ApplicationScoped;
import java.util.LinkedList;
import java.util.List;

@ApplicationScoped
public class ShelfHelper extends MappingHelper{
    public ShelfDto toDto(Shelf shelf){
        var shelfDto = om.toDTO(shelf);
        shelfDto.setRegal_id(shelf.getId());
        shelfDto.setWare_id(shelfDto.getWare_id());
        return shelfDto;
    }
    public List<ShelfDto> toDto(List<Shelf> shelves){
        var shelfDtos = new LinkedList<ShelfDto>();

        for(var shelf : shelves){
            shelfDtos.add(toDto(shelf));
        }
        return shelfDtos;
    }

    public Shelf fromDto(ShelfDto dto){
        return om.fromDto(dto);
    }

    public List<Shelf> fromDto(List<ShelfDto> dtos){
        var result = new LinkedList<Shelf>();
        for (var dto : dtos) {
            result.add(fromDto(dto));
        }
        return result;
    }
}