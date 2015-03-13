using UnityEngine;
using System.Collections;

public class CollIterator <T>{

	public int size;
	public int puntero;


	public int maxSize;

	private T[] list;

	public CollIterator(int max){
		puntero = 0;
		size=0;
		maxSize = max;
		list = new T[maxSize];

	}

	public T insert(T obj){ // inserta el objeto en el puntero y devuelve el objeto que estaba antes en esa posicion(null si no habia nada)
		T aux = list[puntero];

		list[puntero]=obj;
		if(aux==null)size++;

		return aux;
	}

	public T getElement(int i){ // devuelve el elemento i y coloca el puntero en su posicion
		if(i<0||i>=maxSize){ return default(T);}
		puntero = i;
		return list[i];
	}

	public T getNext(){ // returns the next filled value(skips null ones!!) and starts from the beginning if it hits the end
		if(puntero<size-1){
			puntero++;
		}else{
			puntero=0;
		}
		return list[puntero];
	}

	public T getPrev(){
		if(puntero>0){
			puntero--;
		}else{
			puntero=size-1;
		}
		return list[puntero];
	}

	public T remove(){ // borra el elemento del puntero y lo devuelve, e puntero se queda en el mismo sitio apuntando a null
		T aux = list[puntero];
		list[puntero]=default(T);
		return aux;
	}

	public bool findFirstEmpty(){ // mueve el puntero a un punto en el que el contenido sea null, devuelve false si no existe(esta lleno)
		int aux = 0;

		while(aux<maxSize&&list[aux]!=null){
			aux++;
		}
		if(aux>=maxSize){
			return false; // el vector esta lleno
		}
		puntero = aux;
		return true;
	}

	public bool isFull(){ // returns true if the array is full
		return size==maxSize;
	}
	
}
