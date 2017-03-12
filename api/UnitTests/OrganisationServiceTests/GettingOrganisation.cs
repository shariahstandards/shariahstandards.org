using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using FakeItEasy;
using NUnit.Framework;
using StoredObjects;
using WebApiResources;

namespace UnitTests.OrganisationServiceTests
{
    public class GettingOrganisation:OrganisationServiceTestContext
    {
        [Test]
        public void ReturnsTheSingleOrganisationWithoutAParentOrganisation()
        {
            MethodToTest(()=>service.GetOrganisation(A<IPrincipal>.Ignored,A<int>.Ignored));

            var organisationId = 2;
            var principal = A.Fake<IPrincipal>();
            var user = new Auth0User();
            A.CallTo(() => dependencies.UserService.GetAuthenticatedUser(principal)).Returns(user);
            var org = new ShurahBasedOrganisation();
            var orgs = A.Fake<IDbSet<ShurahBasedOrganisation>>();
            A.CallTo(() => dependencies.StorageService.SetOf<ShurahBasedOrganisation>()).Returns(orgs);
            A.CallTo(() => dependencies.LinqService.Single(orgs,
                A<Expression<Func<ShurahBasedOrganisation, bool>>>.That.Matches(x =>
                    x.Compile().Invoke(new ShurahBasedOrganisation
                    {
                        Id = organisationId
                    })
                    && !x.Compile().Invoke(new ShurahBasedOrganisation
                    {
                        Id = organisationId+1
                    })
                    ))).Returns(org);
            var organisationResource = new OrganisationResource();
            A.CallTo(() => service.BuildOrganisationResource(org, user)).Returns(organisationResource);

            var result = service.GetOrganisation(principal,organisationId);

            Assert.AreSame(organisationResource,result);
        }

        public class BuildingOrganisationResource : OrganisationServiceTestContext
        {
            [Test]
            public void PropertiesAerBuiltCorrectly()
            {
                MethodToTest(()=>service.BuildOrganisationResource(A<ShurahBasedOrganisation>.Ignored,A<Auth0User>.Ignored));

                var user = new Auth0User
                {
                    
                };
                var org = new ShurahBasedOrganisation
                {
                    Name = "a",
                    Description = "B",
                    JoiningPolicy = JoiningPolicy.NoApplicationNeeded,
                    Members = new List<Member>(),
                    MembershipRuleSections = new List<MembershipRuleSection>(),
                    Terms = new List<MembershipRuleTermDefinition>(),
                    
                };
                var sortedTerms = new List<MembershipRuleTermDefinition>();
                A.CallTo(() => service.SortTerms(org.Terms)).Returns(sortedTerms);
                var memberResource = new MemberResource();
                A.CallTo(() => service.GetMember(user, org.Members)).Returns(memberResource);
                var ruleSectionResources = new List<MembershipRuleSectionResource>();
                var filteredSections = new List<MembershipRuleSection>();
                A.CallTo(() => dependencies.LinqService.Where(org.MembershipRuleSections,
                    A<Func<MembershipRuleSection, bool>>.That.Matches(x =>
                        x.Invoke(new MembershipRuleSection())
                        &&
                        !x.Invoke(new MembershipRuleSection
                        {
                            ParentMembershipRuleSection = new MembershipRuleSectionRelationship()
                        })
                        ))).Returns(filteredSections);

                A.CallTo(() => service.BuildMembershipRuleSectionResources(String.Empty,filteredSections,sortedTerms,user))
                    .Returns(ruleSectionResources);

                var result = service.BuildOrganisationResource(org, user);

                Assert.AreEqual(org.Name,result.Name);
                Assert.AreEqual(org.Description,result.Description);
                Assert.AreEqual(org.JoiningPolicy.ToString(),result.JoiningPolicy);
                Assert.AreEqual(memberResource,result.Member);
                Assert.AreSame(ruleSectionResources,result.RuleSections);
            }

            public class SortingTerms : OrganisationServiceTestContext
            {
                [Test]
                public void TermsAreOrderedByLengthThenAlphabetically()
                {
                    MethodToTest(()=>service.SortTerms(A<IEnumerable<MembershipRuleTermDefinition>>.Ignored));

                    var unsortedTerms = new List<MembershipRuleTermDefinition>();
                    var sortedByTermLengthTerms = A.Fake<IOrderedEnumerable<MembershipRuleTermDefinition>>();

                    A.CallTo(() => dependencies.LinqService.OrderBy(unsortedTerms,
                                A<Func<MembershipRuleTermDefinition, int>>.That.Matches(x =>
                                    x.Invoke(new MembershipRuleTermDefinition
                                    {
                                        Term = "1234"
                                    }) == 4
                                    ))).Returns(sortedByTermLengthTerms);
                    var sortedByTextTerms = A.Fake<IOrderedEnumerable<MembershipRuleTermDefinition>>();

                    A.CallTo(() => dependencies.LinqService.ThenBy(sortedByTermLengthTerms,
                                A<Func<MembershipRuleTermDefinition, string>>.That.Matches(x =>
                                    x.Invoke(new MembershipRuleTermDefinition
                                    {
                                        Term = "1234"
                                    }) == "1234"
                                    ))).Returns(sortedByTextTerms);
                    var sortedTerms = new List<MembershipRuleTermDefinition>();
                    A.CallTo(() => dependencies.LinqService.EnumerableToList(sortedByTextTerms)).Returns(sortedTerms);

                    var result = service.SortTerms(unsortedTerms);

                    Assert.AreSame(sortedTerms,result);

                }
            }
            public class GettingMember : OrganisationServiceTestContext
            {
                public class Auth0UserNull : OrganisationServiceTestContext
                {
                    [Test]
                    public void NullReturned()
                    {
                        MethodToTest(() => service.GetMember(A<Auth0User>.Ignored, A<IEnumerable<Member>>.Ignored));

                        var members = new List<Member>();

                        
                        var result = service.GetMember(null, members);

                        Assert.IsNull(result);

                    }
                }
                public class Auth0UserIsAMember : OrganisationServiceTestContext
                {
                    [Test]
                    public void MemberReturned()
                    {
                        MethodToTest(()=>service.GetMember(A<Auth0User>.Ignored,A<IEnumerable<Member>>.Ignored));

                        var members = new List<Member>();
                        var user = new Auth0User {Id = "someid"};

                        var exampleMatchingMember = new Member
                        {
                            MemberAuth0Users = new List<MemberAuth0User>
                            {
                                new MemberAuth0User {Auth0UserId = user.Id}
                            }
                        };
                        var exampleNonMatchingMember = new Member
                        {
                            MemberAuth0Users = new List<MemberAuth0User>
                            {
                                new MemberAuth0User {Auth0User = new Auth0User()}
                            }
                        };
                        var member = new Member();
                        A.CallTo(() => dependencies.LinqService.SingleOrDefault(members,
                            A<Func<Member, bool>>.That.Matches(x =>
                                x.Invoke(exampleMatchingMember)
                                && !x.Invoke(exampleNonMatchingMember)
                                ))).Returns(member);
                        var memberResource = new MemberResource();
                        A.CallTo(() => service.BuildMemberResource(member)).Returns(memberResource);

                        var result = service.GetMember(user, members);

                        Assert.AreSame(memberResource,result);

                    }
                }
                public class Auth0UserIsNotAAMember : OrganisationServiceTestContext
                {
                    [Test]
                    public void NullReturned()
                    {
                        MethodToTest(() => service.GetMember(A<Auth0User>.Ignored, A<IEnumerable<Member>>.Ignored));

                        var members = new List<Member>();
                        var user = new Auth0User { Id = "someid" };

                        var exampleMatchingMember = new Member
                        {
                            MemberAuth0Users = new List<MemberAuth0User>
                            {
                                new MemberAuth0User {Auth0UserId = user.Id}
                            }
                        };
                        var exampleNonMatchingMember = new Member
                        {
                            MemberAuth0Users = new List<MemberAuth0User>
                            {
                                new MemberAuth0User {Auth0User = new Auth0User()}
                            }
                        };
                        A.CallTo(() => dependencies.LinqService.SingleOrDefault(members,
                            A<Func<Member, bool>>.That.Matches(x =>
                                x.Invoke(exampleMatchingMember)
                                && !x.Invoke(exampleNonMatchingMember)
                                ))).Returns(null);

                        var result = service.GetMember(user, members);

                        Assert.IsNull(result);

                    }
                }

                public class BuildingMemberResource : OrganisationServiceTestContext
                {
                    [Test]
                    public void PropertiesAreBuiltCorrectly()
                    {
                        MethodToTest(()=>service.BuildMemberResource(A<Member>.Ignored));
                        var member = new Member
                        {
                            Id=123,
                            LeaderRecognition = new LeaderRecognition { RecognisedLeaderMember = new Member { PublicName="Mr Smith"} },
                            Followers = new List<LeaderRecognition>(),
                            ActionUpdates = new List<ActionUpdate>(),
                            PublicName = "some name",
                            MemberAuth0Users = new List<MemberAuth0User> { new MemberAuth0User {Auth0User = new Auth0User { PictureUrl = "pic"} } }
                            
                        };
                        A.CallTo(() => dependencies.LinqService.EnumerableCount(member.Followers)).Returns(12);
                        A.CallTo(() => dependencies.LinqService.EnumerableSum(member.Followers,
                            A<Func<LeaderRecognition, int>>.That.Matches(x =>
                                x.Invoke(new LeaderRecognition {FolloweCount = 89}) == 89))).Returns(108);
                        A.CallTo(() => service.GetPendingActionsCount(member.ActionUpdates)).Returns(2);

                        var result = service.BuildMemberResource(member);

                        Assert.AreEqual(member.Id,result.Id);
                        Assert.AreEqual(12,result.DirectFollowers);
                        Assert.AreEqual(108,result.IndirectFollowers);
                        Assert.AreEqual(member.Id,result.Id);
                        Assert.AreEqual(member.Id,result.Id);
                        Assert.AreEqual(2,result.ToDoCount);
                        Assert.AreEqual(member.LeaderRecognition.RecognisedLeaderMember.PublicName,result.LeaderPublicName);
                        Assert.AreEqual(member.PublicName,result.PublicName);
                        Assert.AreEqual("pic",result.PictureUrl);
                    }

                    public class MemberRecognisesAnotherMemberAsARepresentative : OrganisationServiceTestContext
                    {
                        [Test]
                        public void PropertiesAreBuiltCorrectly()
                        {
                            MethodToTest(() => service.BuildMemberResource(A<Member>.Ignored));
                            var member = new Member
                            {
                                Id = 123,
                                Followers = new List<LeaderRecognition>(),
                                ActionUpdates = new List<ActionUpdate>(),
                                PublicName = "some name",
                                MemberAuth0Users = new List<MemberAuth0User> { new MemberAuth0User { Auth0User = new Auth0User { PictureUrl = "pic" } } }
                            };
                            A.CallTo(() => dependencies.LinqService.EnumerableCount(member.Followers)).Returns(12);
                            A.CallTo(() => dependencies.LinqService.EnumerableSum(member.Followers,
                                A<Func<LeaderRecognition, int>>.That.Matches(x =>
                                    x.Invoke(new LeaderRecognition { FolloweCount = 89 }) == 89))).Returns(108);
                            A.CallTo(() => service.GetPendingActionsCount(member.ActionUpdates)).Returns(2);

                            var result = service.BuildMemberResource(member);

                            Assert.AreEqual(member.Id, result.Id);
                            Assert.AreEqual(12, result.DirectFollowers);
                            Assert.AreEqual(108, result.IndirectFollowers);
                            Assert.AreEqual(member.Id, result.Id);
                            Assert.AreEqual(member.Id, result.Id);
                            Assert.AreEqual(2, result.ToDoCount);
                            Assert.IsNull(result.LeaderPublicName);
                            Assert.AreEqual(member.PublicName, result.PublicName);
                        }



                    }
                }
            }

            public class BuildingMembershipRuleSectionResources : OrganisationServiceTestContext
            {
                [Test]
                public void BuildsResourcesPerSection()
                {
                    MethodToTest(()=>service.BuildMembershipRuleSectionResources(A<string>.Ignored,
                        A<IEnumerable<MembershipRuleSection>>.Ignored,
                        A<IEnumerable<MembershipRuleTermDefinition>>.Ignored, A<Auth0User>.Ignored));

                    var sectionPrefix = "pre";
                    var user = new Auth0User();
                    var org = new ShurahBasedOrganisation
                    {
                        MembershipRuleSections = new List<MembershipRuleSection>(),
                        Terms = new List<MembershipRuleTermDefinition>()
                    };
                    var exampleIndex = 3;
                    var exampleSection = new MembershipRuleSection();
                    var exampleMembershipRuleSectionResource = new MembershipRuleSectionResource();
                    A.CallTo(()=>service.BuildMembershipRuleSectionResource(sectionPrefix,exampleSection, user,exampleIndex,org.Terms))
                        .Returns(exampleMembershipRuleSectionResource);
                    var orderedSections = A.Fake<IOrderedEnumerable<MembershipRuleSection>>();
                    A.CallTo(() => dependencies.LinqService.OrderBy(org.MembershipRuleSections,
                        A<Func<MembershipRuleSection, int>>.That.Matches(x =>
                            x.Invoke(new MembershipRuleSection {Sequence = 4}) == 4
                            ))).Returns(orderedSections);
                    var resources = new List<MembershipRuleSectionResource>();
                    A.CallTo(() => dependencies.LinqService.SelectIndexedEnumerable(orderedSections,
                        A<Func<MembershipRuleSection, int, MembershipRuleSectionResource>>.That.Matches(x =>
                            x.Invoke(exampleSection, exampleIndex) == exampleMembershipRuleSectionResource
                            ))).Returns(resources);
                    var resourcesList = new List<MembershipRuleSectionResource>();
                    A.CallTo(() => dependencies.LinqService.EnumerableToList(resources)).Returns(resourcesList);



                    var result = service.BuildMembershipRuleSectionResources(sectionPrefix,org.MembershipRuleSections,org.Terms, user);

                    Assert.AreSame(resourcesList,result);
                }
            }

            public class BuildingMembershipRuleSectionResource : OrganisationServiceTestContext
            {
                [Test]
                public void AddsRulesWithTermsAndSubsections()
                {
                    MethodToTest(()=>service.BuildMembershipRuleSectionResource(A<string>.Ignored,A<MembershipRuleSection>.Ignored,
                        A<Auth0User>.Ignored,A<int>.Ignored,A<List<MembershipRuleTermDefinition>>.Ignored));

                    var sectionPrefix = "pre";
                    var sectionIndex = 3;
                    var section = new MembershipRuleSection
                    {
                        MembershipRules = new List<MembershipRule>(),
                        ChildMembershipRuleSections = new List<MembershipRuleSectionRelationship>()
                    };
                    var user = new Auth0User();
                    var terms = new List<MembershipRuleTermDefinition>();

                    var orderedRules = A.Fake<IOrderedEnumerable<MembershipRule>>();
                    A.CallTo(() => dependencies.LinqService.OrderBy(section.MembershipRules,
                        A<Func<MembershipRule, int>>.That.Matches(x =>
                            x.Invoke(new MembershipRule {Sequence = 34}) == 34
                            ))).Returns(orderedRules);
                    var exampleRule = new MembershipRule();
                    var exampleRuleIndex = 4;
                    var rulePrefix = sectionPrefix + (sectionIndex + 1) + ".";
                    var exampleMembershipRuleResource = new MembershipRuleResource();
                    A.CallTo(() => service.BuildMembershipRuleResource(rulePrefix, user, exampleRule, exampleRuleIndex,terms))
                        .Returns(exampleMembershipRuleResource);
                    var ruleResources = new List<MembershipRuleResource>();
                    A.CallTo(() => dependencies.LinqService.SelectIndexedEnumerable(orderedRules,
                        A<Func<MembershipRule,int, MembershipRuleResource>>.That.Matches(x =>
                            x.Invoke(exampleRule,exampleRuleIndex) == exampleMembershipRuleResource
                            ))).Returns(ruleResources);
                    var ruleResourcesList = new List<MembershipRuleResource>();
                    A.CallTo(() => dependencies.LinqService.EnumerableToList(ruleResources)).Returns(ruleResourcesList);
                    var childRuleSections = new List<MembershipRuleSection>();
                    var exampleRuleSectionRelationship = new MembershipRuleSectionRelationship
                    {
                        MembershipRuleSection = new MembershipRuleSection()
                    };
                    A.CallTo(() => dependencies.LinqService.SelectEnumerable(section.ChildMembershipRuleSections,
                        A<Func<MembershipRuleSectionRelationship, MembershipRuleSection>>.That.Matches(x =>
                            x.Invoke(exampleRuleSectionRelationship) ==
                            exampleRuleSectionRelationship.MembershipRuleSection
                            ))).Returns(childRuleSections);
                    var childResourceSections = new List<MembershipRuleSectionResource>();
                    A.CallTo(() => service.BuildMembershipRuleSectionResources(
                        rulePrefix, childRuleSections, terms, user))
                        .Returns(childResourceSections);

                    var result = service.BuildMembershipRuleSectionResource(sectionPrefix,section, user, sectionIndex, terms);

                    Assert.AreSame(ruleResourcesList,result.Rules);
                    Assert.AreSame(childResourceSections,result.SubSections);



                }

                public class BuildingMembershipRuleResource : OrganisationServiceTestContext
                {
                    [Test]
                    public void PropertiesAreCorrectlyBuiltForTheRule()
                    {
                        MethodToTest(()=>service.BuildMembershipRuleResource(A<string>.Ignored,
                            A<Auth0User>.Ignored,A<MembershipRule>.Ignored,A<int>.Ignored,A<IEnumerable<MembershipRuleTermDefinition>>.Ignored));

                        var prefix = "1.2.3.";
                        var index = 7;
                        var rule = new MembershipRule
                        {
                            RuleStatement = "some statement",
                            Id = 67,
                     //       Explanation = new MembershipRuleExplanation { ExplanationUrl = "someurl"},
                            MembershipRuleComprehensionQuestions = new List<MembershipRuleComprehensionQuestion>(),
                            PublishedDateTimeUtc = DateTime.UtcNow
                        };
                        var user = new Auth0User();
                        var fragments = new List<TextFragmentResource>();
                        var terms = new List<MembershipRuleTermDefinition>();
                        A.CallTo(() => service.ParseRuleStatement(rule.RuleStatement,terms)).Returns(fragments);
                        A.CallTo(() => service.GetComprehensionScore(rule, user)).Returns(15);
                        A.CallTo(() => dependencies.LinqService.EnumerableCount(
                            rule.MembershipRuleComprehensionQuestions)).Returns(20);

                        var result = service.BuildMembershipRuleResource(prefix, user, rule, index, terms);

                        Assert.AreEqual(prefix+(index+1),result.Number);
                        Assert.AreEqual(rule.Id,result.Id);
                        Assert.AreSame(fragments,result.RuleFragments);
                 //       Assert.AreEqual(rule.Explanation.ExplanationUrl,result.ExplanationUrl);
                        Assert.AreEqual(15,result.ComprehensionScore);
                        Assert.AreEqual(20,result.MaxComprehensionScore);
                        Assert.AreEqual(rule.PublishedDateTimeUtc.ToString("s"),result.PublishedUtcDateTimeText);
                        Assert.AreEqual(rule.RuleStatement,result.RuleStatement);

                    }
                }

                public class ParsingRuleStatement : OrganisationServiceTestContext
                {
                    [Test]
                    public void AppliesEachTermToRuleStatement()
                    {
                        MethodToTest(()=>service.ParseRuleStatement(A<string>.Ignored,A<IEnumerable<MembershipRuleTermDefinition>>.Ignored));

                        var terms = new List<MembershipRuleTermDefinition>();
                        var statement = "some rule";

                        var initialFragmentList = new List<TextFragmentResource>();
                        A.CallTo(() => service.CreateNewFragmentList(statement)).Returns(initialFragmentList);
                       
                        var fragments = new List<TextFragmentResource>();
                        A.CallTo(() => dependencies.LinqService.Aggregate(terms,
                           initialFragmentList, service.ApplyTerm)).Returns(fragments);
                     //   var exampleParsedFragments = new List<RuleFragmentResource>();
                      //  var exampleFragment = new RuleFragmentResource {Text = "some text"};
                        var parsedFragments = new List<TextFragmentResource>();
                        //A.CallTo(() => service.ParseForQuranReferences(exampleFragment.Text))
                        //    .Returns(exampleParsedFragments);
                      //  A.CallTo(() => dependencies.LinqService.SelectMany(fragments,
                            //A<Func<RuleFragmentResource, IEnumerable<RuleFragmentResource>>>.That.Matches(x =>
                            //    x.Invoke(exampleFragment) == exampleParsedFragments))).Returns(parsedFragments);
                        A.CallTo(() => dependencies.LinqService.SelectMany(fragments, service.AddQuranReferences))
                            .Returns(parsedFragments);
                        var parsedFragmentsList = new List<TextFragmentResource>();
                        A.CallTo(() => dependencies.LinqService.EnumerableToList(parsedFragments))
                            .Returns(parsedFragmentsList);

                        var result = service.ParseRuleStatement(statement, terms);

                        Assert.AreSame(parsedFragmentsList,result);

                    }

                    public class CreatingANewFragmentList : OrganisationServiceTestContext
                    {
                        [Test]
                        public void ReturnsListWithOneFragmentForTheStatement()
                        {
                            MethodToTest(()=>service.CreateNewFragmentList(A<string>.Ignored));

                            var statement = "some statement";

                            var result = service.CreateNewFragmentList(statement);

                            Assert.AreEqual(1,result.Count);
                            Assert.AreEqual(statement,result[0].Text);
                            Assert.IsTrue(result[0].IsPlainText);

                        }
                    }

                    public class ApplyingATerm : OrganisationServiceTestContext
                    {
                        [Test]
                        public void BuildsAFragmentListBySplittingWithTheTerm()
                        {
                            MethodToTest(()=>service.ApplyTerm(A<List<TextFragmentResource>>.Ignored,A<MembershipRuleTermDefinition>.Ignored));

                            var fragments = new List<TextFragmentResource>();
                            var term = new MembershipRuleTermDefinition();

                            var exampleFragment=new TextFragmentResource();
                            var exampleReturnedFragmentList = new List<TextFragmentResource>();
                            A.CallTo(() => service.Split(exampleFragment, term)).Returns(exampleReturnedFragmentList);

                            var fragmentsAfterSplit = new List<TextFragmentResource>();
                            A.CallTo(() => dependencies.LinqService.SelectMany(fragments,
                                A<Func<TextFragmentResource, IEnumerable<TextFragmentResource>>>.That.Matches(x =>
                                    x.Invoke(exampleFragment) == exampleReturnedFragmentList
                                    ))).Returns(fragmentsAfterSplit);
                            var fragmentsAfterSplitList = new List<TextFragmentResource>();
                            A.CallTo(() => dependencies.LinqService.EnumerableToList(fragmentsAfterSplit))
                                .Returns(fragmentsAfterSplitList);

                            var result = service.ApplyTerm(fragments, term);

                            Assert.AreSame(fragmentsAfterSplitList,result);
                        }
                    }

                    public class SplitingARuleFragment : OrganisationServiceTestContext
                    {
                        public class FragmentIsNotPlainText : OrganisationServiceTestContext
                        {
                            [Test]
                            public void ListContainsTheOriginalFragmentOnly()
                            {
                                MethodToTest(()=>service.Split(A<TextFragmentResource>.Ignored,A<MembershipRuleTermDefinition>.Ignored));

                                var fragment = new TextFragmentResource
                                {
                                    IsPlainText = false
                                };
                                var term = new MembershipRuleTermDefinition();

                                var result = service.Split(fragment, term);

                                Assert.AreEqual(1,result.Count);
                                Assert.AreSame(fragment,result.First());
                            }
                        }

                        public class FragmentIsPlainText : OrganisationServiceTestContext
                        {
                            public class TermHasNoMatch : OrganisationServiceTestContext
                            {
                                [Test]
                                public void ListContainsTheOriginalFragmentOnly()
                                {
                                    MethodToTest(() => service.Split(A<TextFragmentResource>.Ignored, A<MembershipRuleTermDefinition>.Ignored));

                                    var fragment = new TextFragmentResource
                                    {
                                        Text = "some rule or other",
                                        IsPlainText = true
                                    };
                                    var term = new MembershipRuleTermDefinition
                                    {
                                        Term = "a term"
                                    };

                                    var result = service.Split(fragment, term);

                                    Assert.AreEqual(1, result.Count);
                                    Assert.AreSame(fragment, result.First());
                                }

                            }
                            public class TermHasAMatchInTheMiddle : OrganisationServiceTestContext
                            {
                                [Test]
                                public void ListContainsThreeFragments()
                                {
                                    MethodToTest(() => service.Split(A<TextFragmentResource>.Ignored, A<MembershipRuleTermDefinition>.Ignored));

                                    var fragment = new TextFragmentResource
                                    {
                                        Text = "some rule or other for a term and another one",
                                        IsPlainText = true
                                    };
                                    var term = new MembershipRuleTermDefinition
                                    {
                                        Term = "A Term",
                                        Id = 687
                                    };

                                    var result = service.Split(fragment, term);

                                    Assert.AreEqual(3, result.Count);
                                    Assert.AreEqual("some rule or other for ",result.First().Text);
                                    Assert.IsTrue(result[0].IsPlainText);
                                    Assert.AreEqual(term.Term, result[1].Text);
                                    Assert.AreEqual(term.Id, result[1].TermId);
                                    Assert.IsFalse(result[1].IsPlainText);
                                    Assert.AreEqual(" and another one", result[2].Text);
                                    Assert.IsTrue(result[2].IsPlainText);
                                }

                            }
                            public class TermHasAMatchInTheEnd : OrganisationServiceTestContext
                            {
                                [Test]
                                public void ListContainsTwoFragments()
                                {
                                    MethodToTest(() => service.Split(A<TextFragmentResource>.Ignored, A<MembershipRuleTermDefinition>.Ignored));

                                    var fragment = new TextFragmentResource
                                    {
                                        Text = "some rule or other for a term",
                                        IsPlainText = true
                                    };
                                    var term = new MembershipRuleTermDefinition
                                    {
                                        Term = "A Term",
                                        Id = 687
                                    };

                                    var result = service.Split(fragment, term);

                                    Assert.AreEqual(2, result.Count);
                                    Assert.AreEqual("some rule or other for ", result.First().Text);
                                    Assert.IsTrue(result[0].IsPlainText);
                                    Assert.AreEqual(term.Term, result[1].Text);
                                    Assert.AreEqual(term.Id, result[1].TermId);
                                    Assert.IsFalse(result[1].IsPlainText);
                                }

                            }
                            public class TermHasAMatchInTheBeginning : OrganisationServiceTestContext
                            {
                                [Test]
                                public void ListContainsThreeFragments()
                                {
                                    MethodToTest(() => service.Split(A<TextFragmentResource>.Ignored, A<MembershipRuleTermDefinition>.Ignored));

                                    var fragment = new TextFragmentResource
                                    {
                                        Text = "a term and another one",
                                        IsPlainText = true
                                    };
                                    var term = new MembershipRuleTermDefinition
                                    {
                                        Term = "A Term",
                                        Id = 687
                                    };

                                    var result = service.Split(fragment, term);

                                    Assert.AreEqual(2, result.Count);
                                    Assert.AreEqual(term.Term, result[0].Text);
                                    Assert.AreEqual(term.Id, result[0].TermId);
                                    Assert.IsFalse(result[0].IsPlainText);
                                    Assert.AreEqual(" and another one", result[1].Text);
                                    Assert.IsTrue(result[1].IsPlainText);
                                }

                            }
                        }
                    }
                }
            }
        }
    }
}
