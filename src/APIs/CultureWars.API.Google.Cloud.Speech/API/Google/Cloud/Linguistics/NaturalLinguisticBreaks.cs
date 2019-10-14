using System;
using System.Collections.Generic;
using System.Text;

namespace CultureWars.API.Google.Cloud.Linguistics
{
	public class NaturalLinguisticBreaks
	{
		/*
		 * should be broken [...] at "naturalistic linguistic breaks"
		 *  - each subtitle forms an understandable segment
		 *
		 * should be broken at "logical points".
		 *  - the ideal line-break will be at a piece of "punctuation"
		 *		- ex: (a full stop); (comma); or (dash);
		 *
		 * if the break has to be elsewhere in the sentence, AVOID SPLITTING the following parts of speech
		 *	- article and noun
		 *		- ex: (the + table); (a + book);
		 *
		 *	- preposition and following phrase
		 *		- ex: (on + the table); (in + a way); (about + his life);
		 *
		 *  - conjunction and following phrase/clause
		 *		- ex: (and + those books); (but + I went there);
		 *
		 *  - pronound and verb
		 *		- ex: (he + is); (they + will come); (it + comes);
		 *
		 *	- parts of a complex verb
		 *		- ex: (have + eaten); (will + have + been _ doing);
		 *
		 * when breaking, should make clear that there is more to come.
		 *	- can be done by ending the first subtitle with the following:
		 *		- ex: ("conjunction"); (colon); (semi-colon); (short run of dots); where appropriate.
		 *
		 *
		 *
		 *
		 */
	}
}
