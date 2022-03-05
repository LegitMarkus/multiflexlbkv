package org.acme.DTO;

import lombok.AllArgsConstructor;
import lombok.Getter;
import lombok.NoArgsConstructor;
import lombok.Setter;

import java.util.List;

@Getter
@Setter
@NoArgsConstructor
@AllArgsConstructor
public class LieferantDto {
    private Integer id;

    private String name;

    private String weblink;

    private Integer lieferzeit;

    private List<Integer> material_ids;
}