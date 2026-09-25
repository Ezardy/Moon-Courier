using UnInventory.Standard.Configuration;
using UnityEngine;


namespace Aniki.Parts {
	internal class MoonCourierDIContainer : ContainerDiStandard {
		[SerializeField] private GameObject	entityPrefab;

		protected override void	BindDataEntitiesToPrefabs() {
			base.BindDataEntitiesToPrefabs();
			BindDataEntityToPrefab<PartSO>(entityPrefab);
		}
	}
}
