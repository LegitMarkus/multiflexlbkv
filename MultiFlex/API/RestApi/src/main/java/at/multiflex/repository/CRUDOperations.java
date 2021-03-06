package at.multiflex.repository;

import javax.enterprise.context.ApplicationScoped;
import javax.inject.Inject;
import javax.persistence.EntityManager;
import javax.transaction.Transactional;

@ApplicationScoped
public class CRUDOperations {
    @Inject
    protected EntityManager em;

    @Transactional
    public<T> void add(T entity){
        if(entity == null) throw new IllegalArgumentException();
        em.persist(entity);
    }
    @Transactional
    public<T> void delete(T entity){
        if(entity == null) throw new IllegalArgumentException();
        em.remove(entity);
    }
    @Transactional
    public<T> void update(T entity){
        if(entity == null) throw new IllegalArgumentException();
        em.merge(entity);
    }
}
