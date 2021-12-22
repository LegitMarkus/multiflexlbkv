package org.acme.model;

import javax.persistence.*;

@Entity
@Table(name = "Fach")
public class Fach {

    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private long fach_id;
    @Column
    private String position;
    @Column
    private long regal_id;
    @Column
    private long ware_id;

    public long getFach_id() {
        return fach_id;
    }

    public void setFach_id(long fach_id) {
        this.fach_id = fach_id;
    }

    public String getPosition() {
        return position;
    }

    public void setPosition(String position) {
        this.position = position;
    }

    public long getRegal_id() {
        return regal_id;
    }

    public void setRegal_id(long regal_id) {
        this.regal_id = regal_id;
    }

    public long getWare_id() {
        return ware_id;
    }

    public void setWare_id(long ware_id) {
        this.ware_id = ware_id;
    }
}