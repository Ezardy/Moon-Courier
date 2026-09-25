using System.Collections.Generic;

namespace Aniki.Parts {
	public interface IPartManipulator {
		public void	Manipulate(IList<PartSO> partHolder, int capacity);
	}
}
